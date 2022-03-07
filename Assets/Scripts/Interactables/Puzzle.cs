using EFK.Interactables;
using EFK.Stats;
using UnityEngine;

namespace SFK.Interactables
{
    public class Puzzle : MonoBehaviour, IInteractable
    {

        [SerializeField] private PuzzleSO puzzle;

        private void Start()
        {
            AddInteractableTag();
        }

        public void AddInteractableTag()
        {
            if (gameObject.tag != "Interactable") gameObject.tag = "Interactable";
        }

        #region IINTERACTABLE
        public void ShowInteraction(BaseStats baseStats)
        {
            Debug.Log("The puzzle shines and has a canvas to interact");
        }

        public void Interact(BaseStats baseStats)
        {
            Instantiate(puzzle.PUZZLECANVAS);
        }
        #endregion

    }

}
