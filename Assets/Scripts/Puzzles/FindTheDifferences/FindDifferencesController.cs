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

    [Header("--- PORTRAIT CHANGE EFFECT --- ")]
    [Space(5)]
    [SerializeField] float portraitChangeDuration;
    [SerializeField] [Range(0,2f)] float distortionAmount;
    [SerializeField] private PortraitDistortion distortionEffect;
    private bool portraitChanging;
    private Coroutine changePortrait_coro;

    //EVENT TRIGGERS
    AddCurseMeter addCurseMeterEvent;

    private void Awake()
    {
        addCurseMeterEvent = new AddCurseMeter(gameSettings.CurseMeterPerMistake);
    }

    public void OnZonePressed(int quadrantId)
    {
        if (CheckGameIsOver() || CheckGameVictory() || portraitChanging) return;

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
        if(changePortrait_coro == null)
        {
            StartCoroutine(ChangePortrait_Coro());
        }
    }

    private IEnumerator ChangePortrait_Coro()
    {
        portraitChanging = true;
        float time = 0;

        //Distort portrait
        while(time < portraitChangeDuration/2)
        {
            time += Time.deltaTime;
            distortionEffect.ChangeDistortion(distortionAmount * time);
            yield return null;
        }

        //change portrait sprite
        currentPortrait = currentPortrait >= 2 ? 0 : currentPortrait + 1;
        differencesCanvas.GetComponent<Image>().sprite = portraitList[currentPortrait].Sprite;

        //undo distortion
        while (time > 0)
        {
            time -= Time.deltaTime;
            distortionEffect.ChangeDistortion(distortionAmount * time);
            yield return null;
        }
        distortionEffect.ChangeDistortion(0);

        portraitChanging = false;
    }
}
