using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindDifferencesController : MonoBehaviour
{
    [SerializeField] PortraitSO[] portraitList;
    [SerializeField] DifferencesSettings gameSettings;

    [SerializeField] private int currentPortrait;

    [SerializeField] private Image differencesCanvas;
    private int currentDifferencesDiscovered;
    private int currentMistakes;

    public void OnZonePressed(int quadrantId)
    {
        Debug.Log("Zone " + quadrantId + " was pressed");
        if (portraitList[currentPortrait].CheckQuadrant(quadrantId) != -1)
        {
            Debug.Log("Difference is correct!");
            currentDifferencesDiscovered++;
            //Mark as solved the difference
        }
        else
        {
            currentMistakes++;
            Debug.Log("Difference is not correct...");
        }

        if (CheckGameVictory())
        {
            Debug.Log("Player wins!");
        }
        else if (CheckGameIsOver())
        {
            Debug.Log("Game Over: Too much mistakes...");
        }
        else
        {
            ChangePortrait();
        }
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
