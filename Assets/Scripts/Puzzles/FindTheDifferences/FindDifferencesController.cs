using RPG.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FindDifferencesController : MonoBehaviour
{
    [Header("--- FADER SETTINGS --- ")]
    [Space(5)]
    [Range(0, 5)]
    [SerializeField] private float fadeOutTime;
    [Range(0, 5)]
    [SerializeField] private float fadeInTime;

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
        Debug.Log("En este puzzle, el castigo por el error es de " + gameSettings.CurseMeterPerMistake);
    }

    public void OnZonePressed(int quadrantId)
    {
        Debug.Log("Zone " + quadrantId + " was pressed");
        if (portraitList[currentPortrait].CheckQuadrant(quadrantId) != -1)
        {
            Debug.Log("Difference is correct!");
            currentDifferencesDiscovered++;
        }
        else
        {
            currentMistakes++;
            EventManager.TriggerEvent(addCurseMeterEvent);
            Debug.Log("Difference is not correct...");
        }

        if (CheckGameVictory())
        {
            Debug.Log("Player wins!");
            FadeOutAndEndGame();
        }
        else if (CheckGameIsOver())
        {
            Debug.Log("Game Over: Too much mistakes...");
            FadeOutAndEndGame();
        }
        else
        {
            ChangePortrait();
        }
    }

    private IEnumerator FadeOutAndEndGame()
    {
        Fader fader = FindObjectOfType<Fader>();

        yield return fader.FadeOut(fadeOutTime);

        yield return new WaitForSeconds(0.5f);

        fader.FadeIn(fadeInTime);

        gameObject.SetActive(false);
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
