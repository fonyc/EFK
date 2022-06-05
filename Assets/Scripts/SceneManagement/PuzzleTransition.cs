using EFK.Control;
using RPG.SceneManagement;
using SFK.Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleTransition : MonoBehaviour
{
    [Header("--- FADE SETTINGS ---")]
    [Range(0.0f, 4.0f)]
    [SerializeField] private float fadeOutTime = 2f;
    [Range(0.0f, 4.0f)]
    [SerializeField] private float fadeInTime = 2f;
    private GameObject gameCanvas;

    #region Listeners

    private UnityAction<object> OnPuzzleEnds;

    #endregion

    private void Awake()
    {
        OnPuzzleEnds = new UnityAction<object>(ManageEvent);
    }
    private void Start()
    {
        //Event subscribe
        Type type = typeof(SolvePuzzle);
        EventManager.StartListening(type, OnPuzzleEnds);
    }
    
    private void ManageEvent(object argument)
    {
        switch (argument)
        {
            case SolvePuzzle varType:
                GetComponent<Puzzle>().IsSolved = true;
                StartCoroutine(FadeInOutPuzzle_Coro(null));
                break;
        }
    }

    public void FadeInOutPuzzle(GameObject puzzleCanvas)
    {
        StartCoroutine(FadeInOutPuzzle_Coro(puzzleCanvas));
    }

    private IEnumerator FadeInOutPuzzle_Coro(GameObject puzzleCanvas)
    {
        Fader fader = FindObjectOfType<Fader>();

        PlayerController ia = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ia.InputActions.Disable();

        yield return fader.FadeOut(fadeOutTime);

        yield return new WaitForSeconds(0.5f);

        fader.FadeIn(fadeInTime);
        ia.InputActions.Enable();

        if(puzzleCanvas != null)
        {
            //Its a game start call
            gameCanvas = Instantiate(puzzleCanvas);
        }
        else
        {
            //Its a game end call
            gameCanvas.SetActive(false);
        }
    }
}
