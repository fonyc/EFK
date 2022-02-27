using EFK.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EFK.Interactables
{
    public class Collectible : MonoBehaviour, IInteractable
    {
        [SerializeField] private int collectibleNumber;

        void Start()
        {
            AddInteractableTag();
        }

        #region IINTERACTABLE

        public void AddInteractableTag()
        {
            if (gameObject.tag != "Interactable") gameObject.tag = "Interactable";
        }

        public void ShowInteraction(CharactersStats playerAtributes)
        {
            Debug.Log("Collectible is shining and has a canvas!");
        }

        public void Interact(CharactersStats playerAtributes)
        {
            Debug.Log("Piece of collection founded");
        }

        #endregion
    }
}
