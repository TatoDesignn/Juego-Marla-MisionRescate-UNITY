using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class CinemachinePOVExt : CinemachineExtension
{
    [SerializeField] private float ClampAngle;
    [SerializeField] private float HorizontalSpeed;
    [SerializeField] private float VerticalSpeed;

    private InputManager inputManager;
    private Vector3 startRotation;

    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase VCam, CinemachineCore.Stage stage, ref CameraState state, float deltatime)
    {
        if (VCam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if(startRotation == null) startRotation = transform.localRotation.eulerAngles;
                Vector2 deltaInput = inputManager.GetMouseDelta();
                startRotation.x += deltaInput.x * VerticalSpeed * Time.deltaTime;
                startRotation.y += deltaInput.y * HorizontalSpeed * Time.deltaTime;
                startRotation.y = Mathf.Clamp(startRotation.y, -ClampAngle, ClampAngle);
                state.RawOrientation = quaternion.Euler(-startRotation.y, startRotation.x, 0f);
            }
               
        }

    }
}
