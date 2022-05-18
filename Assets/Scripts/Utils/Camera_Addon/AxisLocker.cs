using Cinemachine;
using UnityEngine;

[ExecuteInEditMode][SaveDuringPlay][AddComponentMenu("")]
public class AxisLocker : CinemachineExtension
{
    [Header("--- CAMERA LOCK SETTINGS ---")]
    [Tooltip("Lock the Camera's X Axis")]
    [SerializeField] private bool lockXAxis;
    [SerializeField] private float m_XPosition = 0;
    [Space(5)]
    [Tooltip("Lock the Camera's Z Axis")]
    [SerializeField] private bool lockZAxis;
    [SerializeField] private float m_ZPosition = 0;

    public bool LockXAxis { get => lockXAxis; set => lockXAxis = value; }
    public float XPosition { get => m_XPosition; set => m_XPosition = value; }
    public bool LockZAxis { get => lockZAxis; set => lockZAxis = value; }
    public float ZPosition { get => m_ZPosition; set => m_ZPosition = value; }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        float xValue;
        float zValue;

        if (stage == CinemachineCore.Stage.Finalize)
        {
            xValue = LockXAxis ? XPosition : state.RawPosition.x;
            zValue = LockZAxis ? ZPosition : state.RawPosition.z;

            state.RawPosition = new Vector3(xValue, state.RawPosition.y, zValue);
        }
    }

}
