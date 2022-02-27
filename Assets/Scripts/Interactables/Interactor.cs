using UnityEngine;
using EFK.Interactables;
using EFK.Stats;

public class Interactor : MonoBehaviour
{
    [SerializeField] private int detectionRadius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            CharactersStats cs = GetComponent<BaseStats>().characters;
            if(cs != null) other.GetComponent<IInteractable>().Interact(cs);
        }
    }

    #region GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    #endregion

}
