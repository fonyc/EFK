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
        public void Interact(CharactersStats playerAtributes)
        {
            Debug.Log("Changing to level: " + level);
        }

        public void AddInteractableTag()
        {
            gameObject.tag = "Interactable";
        }
        #endregion
    }

}
