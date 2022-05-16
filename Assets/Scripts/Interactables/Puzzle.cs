using EFK.Control;
using EFK.Interactables;
using EFK.Stats;
using RPG.SceneManagement;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SFK.Interactables
{
    public class Puzzle : MonoBehaviour, IInteractable
    {
        [Header("--- FADE SETTINGS ---")]
        [Range(0.0f, 4.0f)]
        [SerializeField] private float fadeOutTime = 2f;
        [Range(0.0f, 4.0f)]
        [SerializeField] private float fadeInTime = 2f;
        [Header("--- PUZZLE SETTINGS ---")]
        [SerializeField] private PuzzleSO puzzle;
        [SerializeField] private bool isSolved;

        #region Listeners

        private UnityAction<object> OnPuzzleEnds;

        #endregion

        private void Awake()
        {
            OnPuzzleEnds = new UnityAction<object>(ManageEvent);
        }

        private void Start()
        {
            AddInteractableTag();

            //Event subscribe
            Type type = typeof(SolvePuzzle);
            EventManager.StartListening(type, OnPuzzleEnds);
        }

        private void ManageEvent(object argument)
        {
            switch (argument)
            {
                case SolvePuzzle varType:
                    SolvePuzzle();
                    break;
            }
        }

        private void SolvePuzzle()
        {
            isSolved = true;
        }

        public void AddInteractableTag()
        {
            if (gameObject.tag != "Interactable") gameObject.tag = "Interactable";
        }

        #region IINTERACTABLE
        public void ShowInteraction(BaseStats baseStats)
        {
            if (isSolved) return;
            Debug.Log("The puzzle shines and has a canvas to interact");
        }

        private IEnumerator FadeOutPuzzle()
        {
            Fader fader = FindObjectOfType<Fader>();

            PlayerController ia = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            ia.InputActions.Disable();

            yield return fader.FadeOut(fadeOutTime);

            yield return new WaitForSeconds(0.5f);

            fader.FadeIn(fadeInTime);
            ia.InputActions.Enable();
            Instantiate(puzzle.PuzzleCanvas);
        }

        public void Interact(BaseStats baseStats)
        {
            if (isSolved) return;
            StartCoroutine(FadeOutPuzzle());
        }

        private void OnDisable()
        {
            Type type = typeof(SolvePuzzle);
            EventManager.StopListening(type, OnPuzzleEnds);
        }
        #endregion
    }
}
