using UnityEngine;
using EFK.Interactables;
using EFK.Stats;

public class Interactor : MonoBehaviour
{
    [SerializeField] private int detectionRadius;
    [SerializeField] IInteractable target;
    public bool hasTarget = false;

    public IInteractable TARGET
    {
        get { return target; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            hasTarget = true;
            target = other.GetComponent<IInteractable>();
            BaseStats baseStats = GetComponent<BaseStats>();
            if(baseStats != null) other.GetComponent<IInteractable>().ShowInteraction(baseStats);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            target = null;
            hasTarget = false;
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
