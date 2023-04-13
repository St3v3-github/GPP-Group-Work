using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    private Vector2 camInput;
    private Vector3 camDirection;
    public LayerMask collisionLayers;
    private Vector3 cameraVectorPosition;

    public Transform targetTransform;
    public Transform cameraPivot;
    public Transform cameraTransform;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private float defaultPosition;

    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;
    public float cameraCollisionArea = 0.2f;
    public float cameraCollisionOffset = 0.2f;
    public float minimumCollisionOffset = 0.2f;

    public float lookAngle;
    public float pivotAngle;
    public float minPivotAngle = -35;
    public float maxPivotAngle = 35;
    void Start()
    {
        //offset = transform.position - target.transform.position;
    }

    private void Awake()
    {
        targetTransform = FindObjectOfType<PlayerController>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    void LateUpdate()
    {
        //if (_input.sqrMagnitude == 0) return;

        //var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        //transform.position = target.transform.position + (targetAngle * offset);
        //transform.LookAt(target.transform);

        //float desiredAngle = _direction.y;
        //Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        //transform.position = target.transform.position + (rotation * offset);
        //transform.LookAt(target.transform);


    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        CameraCollisions();
    }

    public void Look(InputAction.CallbackContext context)
    {
        camInput = context.ReadValue<Vector2>();
        camDirection = new Vector3(camInput.x, camInput.y, 0.0f);
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = lookAngle + (camDirection.x * cameraLookSpeed);
        pivotAngle = pivotAngle - (camDirection.y * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void CameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionArea, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffset);
        }

        if(Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = targetPosition - minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }

}
