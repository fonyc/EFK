using EFK.Interactables;
using EFK.Stats;
using UnityEngine;

namespace SFK.Interactables
{
    public class Puzzle : MonoBehaviour, IInteractable
    {
        private void Start()
        {
            AddInteractableTag();
        }

        public void AddInteractableTag()
        {
            if (gameObject.tag != "Interactable") gameObject.tag = "Interactable";
        }

        #region IINTERACTABLE
        public void ShowInteraction(CharactersStats playerAtributes)
        {
            //TODO --> 
            Debug.Log("The puzzle shines and has a canvas to interact");
        }

        public void Interact(CharactersStats playerAtributes)
        {
            Debug.Log("Opening Puzzle");
        }
        #endregion

    }

}
