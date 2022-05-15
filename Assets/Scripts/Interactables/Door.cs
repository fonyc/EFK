using EFK.Interactables;
using EFK.Stats;
using RPG.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace SFK.Interactables
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private int level;

        [Header("--- Strategic Points ---")]
        [SerializeField] private Collider invisibleCollider;

        [Header("--- VFX ---")]
        [Space(5)]
        [SerializeField] private GameObject closed_VFX;
        [SerializeField] private GameObject open_VFX;

        private void Start()
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
            Debug.Log("Door is shining and showing a canvas");
        }

        public void Interact(BaseStats baseStats)
        {
            Debug.Log("Changing to level: " + level);
            GetComponent<Door_Teleporter>().SceneTransition_Coro(level);
        }
        #endregion
    }

}
