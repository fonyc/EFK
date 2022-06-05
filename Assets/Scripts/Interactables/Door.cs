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

        [Header("--- INTERACTION VFX ---")]
        [Space(5)]
        [SerializeField] private Sprite doorIcon;
        
        [SerializeField] private bool isOpened;

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
                    OpenDoor();
                    break;
            }
        }

        private void OpenDoor()
        {
            isOpened = true;
        }

        #region IINTERACTABLE
        public void AddInteractableTag()
        {
            if (gameObject.tag != "Interactable") gameObject.tag = "Interactable";
        }

        public void ShowInteraction(BaseStats baseStats)
        {
            //if (!isOpened) return;
            ShowInteraction showInteraction = new ShowInteraction(doorIcon);
            EventManager.TriggerEvent(showInteraction);
            Debug.Log("Door is shining and showing a canvas");
        }

        public void Interact(BaseStats baseStats)
        {
            //if (!isOpened) return;
            Debug.Log("Changing to level: " + level);
            GetComponent<Door_Teleporter>().SceneTransition_Coro(level);
        }
        #endregion
    }

}
