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

        public void ShowInteraction(BaseStats baseStats)
        {
            if (baseStats.CHARACTERTYPE != CharacterType.MaryJaneGraham) return;

            Debug.Log("The collection piece is shining");
        }

        public void Interact(BaseStats baseStats)
        {
            if (baseStats.CHARACTERTYPE != CharacterType.MaryJaneGraham) return;

            Debug.Log("Piece of collection founded");
        }

        #endregion
    }
}
