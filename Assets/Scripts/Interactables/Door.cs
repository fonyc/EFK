using EFK.Interactables;
using EFK.Stats;
using UnityEngine;

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

        private void ActivateCollider(bool value) => invisibleCollider.enabled = value;

        #region IINTERACTABLE
        public void ShowInteraction(CharactersStats playerAtributes)
        {
            //TO DO --> Call Scene management and leave scene
            Debug.Log("Door is shining and showing a canvas");
        }

        public void AddInteractableTag()
        {
            if (gameObject.tag != "Interactable") gameObject.tag = "Interactable";
        }

        public void Interact(CharactersStats playerAtributes)
        {
            Debug.Log("Changing to level: " + level);
        }
        #endregion
    }

}
