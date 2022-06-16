using EFK.Control;
using RPG.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindDifferencesController : MonoBehaviour
{
    [Header("--- PUZZLE SETTINGS --- ")]
    [Space(5)]
    [SerializeField] PortraitSO[] portraitList;
    [SerializeField] DifferencesSettings gameSettings;

    [SerializeField] private Transform differencesCanvas;

    private int currentPortrait;
    private int currentDifferencesDiscovered;
    private int currentMistakes;

    [Header("--- PORTRAIT CHANGE EFFECT --- ")]
    [Space(5)]
    [SerializeField] float portraitChangeDuration;
    [SerializeField][Range(0, 2f)] float distortionAmount;
    [SerializeField] private PortraitDistortion distortionEffect;
    [SerializeField] float timeBetweenClickAndChange;
    private bool portraitChanging;

    //Variables to count succeses
    private bool[,] solvedList;

    //EVENT TRIGGERS
    AddCurseMeter addCurseMeterEvent;
    PaintError paintErrorEvent;

    private void Awake()
    {
        paintErrorEvent = new PaintError();
        addCurseMeterEvent = new AddCurseMeter(gameSettings.CurseMeterPerMistake);
        solvedList = new bool[portraitList.Length, portraitList[0].DifferenceList.Count];
    }

    private void Start()
    {
        //Init the solutions to false
        InitSolvedList();
    }

    public void OnZonePressed(int quadrantId)
    {
        if (CheckGameIsOver() || CheckGameVictory() || portraitChanging) return;

        int quadrantIndex = portraitList[currentPortrait].CheckQuadrant(quadrantId);

        if (quadrantIndex != -1)
        {
            currentDifferencesDiscovered++;

            //Add success to the solved list
            solvedList[currentPortrait, quadrantIndex] = true;

            //Activate visual feedback
            differencesCanvas.GetChild(quadrantId).GetChild(0).gameObject.SetActive(true);

            if (CheckGameVictory())
            {
                TriggerPuzzleEnds();
            }
        }
        else
        {
            currentMistakes++;

            //Show error in canvas
            EventManager.TriggerEvent(paintErrorEvent);

            //Add curse meter
            EventManager.TriggerEvent(addCurseMeterEvent);

            if (CheckGameIsOver())
            {
                TriggerPuzzleEnds();
            }
        }

        StartCoroutine(ChangePortrait_Coro());
    }

    private void TriggerPuzzleEnds()
    {
        ShowInteraction showInteraction = new ShowInteraction(null);
        EventManager.TriggerEvent(showInteraction);

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

    private IEnumerator ChangePortrait_Coro()
    {
        portraitChanging = true;
        yield return new WaitForSeconds(timeBetweenClickAndChange);

        ActivateSuccesInPortrait(false);

        float time = 0;

        //Distort portrait
        while (time < portraitChangeDuration / 2)
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

        //Change different visual feedbacks
        ActivateSuccesInPortrait(true);

        portraitChanging = false;
    }

    private void ActivateSuccesInPortrait(bool value)
    {
        for (int i = 0; i < portraitList[0].DifferenceList.Count; i++)
        {
            if (solvedList[currentPortrait, i] == true)
            {
                differencesCanvas.GetChild(portraitList[currentPortrait].DifferenceList[i]).GetChild(0).gameObject.SetActive(value);
            }
        }
    }

    private void InitSolvedList()
    {
        for (int x = 0; x < portraitList.Length; x++)
        {
            for (int i = 0; i < portraitList[0].DifferenceList.Count; i++)
            {
                solvedList[x, i] = false;
            }
        }
    }
}
