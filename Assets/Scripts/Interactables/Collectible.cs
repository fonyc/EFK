using EFK.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EFK.Interactables
{
    public class Collectible : MonoBehaviour, IInteractable
    {
        [SerializeField] private int collectibleNumber;

        [Header("--- PUZZLE VISUAL INTERACTIONS ---")]
        [Space(5)]
        [SerializeField] Sprite cameraIcon;

        void Start()
        {
            AddInteractableTag();
        }

        #region IINTERACTABLE

        public void AddInteractableTag()
        {
            if (gameObject.tag != "Interactable") gameObject.tag = "Interactable";
        }

        public void ShowInteraction(BaseStats baseStats)
        {
            ShowInteraction showInteraction = new ShowInteraction(cameraIcon);
            EventManager.TriggerEvent(showInteraction);

            Debug.Log("The collection piece is shining");
        }

        public void Interact(BaseStats baseStats)
        {
            Debug.Log("Piece of collection founded");
        }

        #endregion
    }
}
