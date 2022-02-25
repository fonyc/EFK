using SFK.Interactables;
using SFK.Stats;
using UnityEngine;

namespace SFK.Interactables
{
    public class ChangeLevel : MonoBehaviour, IInteractable
    {
        [SerializeField] private int level;

        [Header("--- Strategic Points ---")]
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private Collider invisibleCollider;

        [Header("--- VFX ---")]
        [Space(5)]
        [SerializeField] private GameObject closed_VFX;
        [SerializeField] private GameObject open_VFX;



        private void ActivateCollider(bool value) => invisibleCollider.enabled = value;

        #region IINTERACTABLE
        public void Interact(CharactersStats playerAtributes)
        {
            //ActivateCollider(true);
        }
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Podemos interactuar, muchacho");
            }
        }

        #region GIZMOS
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(startPoint.position, 0.5f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(endPoint.position, 0.5f);
        }
        #endregion
    }

}
