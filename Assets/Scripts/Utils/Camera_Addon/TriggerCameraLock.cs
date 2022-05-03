using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggerCameraLock : MonoBehaviour
{
    [SerializeField] private Axis axis;
    [SerializeField] private AxisLocker axisLocker;

    [SerializeField] private float cameraOffsetZ;
    //Z offset seems to be calculated with a +0.3f imprecision wich makes transitions very bad
    [Tooltip("Corrects the error obtained from the trigger")]
    [SerializeField] private float deltaError;
    [SerializeField] CinemachineVirtualCamera vcam;
    private int lockedTimes;

    private void Start()
    {
        CinemachineComponentBase body = vcam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (body is CinemachineFramingTransposer)
        {
            cameraOffsetZ = (body as CinemachineFramingTransposer).m_CameraDistance; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            lockedTimes++;
            if (lockedTimes == 1)
            {
                if (axis == Axis.X)
                {
                    axisLocker.LockXAxis = true;
                    axisLocker.XPosition = other.transform.position.x;
                }
                else if (axis == Axis.Z)
                {
                    axisLocker.LockZAxis = true;
                    axisLocker.ZPosition = other.transform.position.z - cameraOffsetZ + deltaError;
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
                    axisLocker.LockXAxis = false;
                }
                else if (axis == Axis.Z)
                {
                    axisLocker.LockZAxis = false;
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
