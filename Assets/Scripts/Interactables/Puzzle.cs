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
            gameObject.tag = "Interactable";
        }

        #region IINTERACTABLE
        public void Interact(CharactersStats playerAtributes)
        {
            Debug.Log("Opening Puzzle");
        }
        #endregion

    }

}
