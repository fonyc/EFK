using EFK.Interactables;
using EFK.Stats;
using System;
using UnityEngine;

namespace SFK.Interactables
{
    public class Puzzle : MonoBehaviour, IInteractable
    {
        [Header("--- PUZZLE SETTINGS ---")]
        [SerializeField] private PuzzleSO puzzle;
        [SerializeField] private bool isSolved;

        public bool IsSolved { get => isSolved; set => isSolved = value; }
        public PuzzleSO PuzzleSO { get => puzzle; set => puzzle = value; }

        private void Start()
        {
            AddInteractableTag();
        }

        private void SolvePuzzle()
        {
            IsSolved = true;
        }

        public void AddInteractableTag()
        {
            if (gameObject.tag != "Interactable") gameObject.tag = "Interactable";
        }

        #region IINTERACTABLE
        public void ShowInteraction(BaseStats baseStats)
        {
            if (IsSolved) return;
            Debug.Log("The puzzle shines and has a canvas to interact");
        }

        public void Interact(BaseStats baseStats)
        {
            if (IsSolved) return;
            PuzzleTransition puzzleTransition = GetComponent<PuzzleTransition>();
            puzzleTransition.FadeInOutPuzzle(PuzzleSO.PuzzleCanvas);
        }

        #endregion
    }
}
