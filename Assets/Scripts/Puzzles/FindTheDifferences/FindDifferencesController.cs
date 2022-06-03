using EFK.Control;
using RPG.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FindDifferencesController : MonoBehaviour
{
    [Header("--- PUZZLE SETTINGS --- ")]
    [Space(5)]
    [SerializeField] PortraitSO[] portraitList;
    [SerializeField] DifferencesSettings gameSettings;

    [SerializeField] private Image differencesCanvas;

    private int currentPortrait;
    private int currentDifferencesDiscovered;
    private int currentMistakes;

    //EVENT TRIGGERS
    AddCurseMeter addCurseMeterEvent;

    private void Awake()
    {
        addCurseMeterEvent = new AddCurseMeter(gameSettings.CurseMeterPerMistake);
    }

    public void OnZonePressed(int quadrantId)
    {
        if (CheckGameIsOver() || CheckGameVictory()) return;

        Debug.Log("Zone " + quadrantId + " was pressed");
        if (portraitList[currentPortrait].CheckQuadrant(quadrantId) != -1)
        {
            Debug.Log("Difference is correct!");
            currentDifferencesDiscovered++;
            if (CheckGameVictory())
            {
                Debug.Log("Player wins!");
                TriggerPuzzleEnds();
            }
        }
        else
        {
            Debug.Log("Difference is not correct...");
            currentMistakes++;
            EventManager.TriggerEvent(addCurseMeterEvent);
            if (CheckGameIsOver())
            {
                Debug.Log("Game Over: Too much mistakes...");
                TriggerPuzzleEnds();
            }
        }

        ChangePortrait();
    }

    private void TriggerPuzzleEnds()
    {
        SolvePuzzle solvePuzzleTrigger = new SolvePuzzle();
        EventManager.TriggerEvent(solvePuzzleTrigger);
    }

    private bool CheckGameIsOver()
    {
        return currentMistakes >= gameSettings.MaxMistakes;
    }

    private bool CheckGameVictory()
    {
        return currentDifferencesDiscovered >= gameSettings.DifferencesToDiscover;
    }

    private void ChangePortrait()
    {
        Debug.Log("Changing portrait...");
        currentPortrait = currentPortrait >= 2 ? 0 : currentPortrait + 1;
        differencesCanvas.GetComponent<Image>().sprite = portraitList[currentPortrait].Sprite;
    }
}
