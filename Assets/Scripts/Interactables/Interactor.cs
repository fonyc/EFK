using UnityEngine;
using EFK.Interactables;
using EFK.Stats;

public class Interactor : MonoBehaviour
{
    [SerializeField] private int detectionRadius;
    [SerializeField] IInteractable target;

    public IInteractable TARGET
    {
        get { return target; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            target = other.GetComponent<IInteractable>();
            CharactersStats cs = GetComponent<BaseStats>().characters;
            if(cs != null) other.GetComponent<IInteractable>().Interact(cs);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }

    #region GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    #endregion

}
