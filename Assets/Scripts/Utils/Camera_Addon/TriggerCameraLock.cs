using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraLock : MonoBehaviour
{
    [SerializeField] private Axis axis;
    [SerializeField] private AxisLocker vcam;
    [SerializeField] private bool hardCodePositionZ;
    [SerializeField] private float hardCodePositionZ_value;
    private int lockedTimes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            lockedTimes++;
            if (lockedTimes == 1)
            {
                if(axis == Axis.X)
                {
                    vcam.LockXAxis = true;
                    vcam.XPosition = other.transform.position.x;
                }
                else if(axis == Axis.Z)
                {
                    vcam.LockZAxis = true;
                    if (hardCodePositionZ) vcam.ZPosition = hardCodePositionZ_value;
                    else
                    {
                        //Z position not working properly when taking it from trigger. 
                        vcam.ZPosition = other.transform.position.z;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger");
            if (lockedTimes == 1)
            {
                lockedTimes--;
                if (axis == Axis.X)
                {
                    vcam.LockXAxis = false;
                }
                else if (axis == Axis.Z)
                {
                    vcam.LockZAxis = false;
                }
            }
            else if (lockedTimes == 2)
            {
                lockedTimes--;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
