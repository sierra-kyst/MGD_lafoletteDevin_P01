using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PinchDetection : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 4f;
    private TouchAction controls;
    private Coroutine zoomCoroutine;
    private Transform cameraTransform;
    private void Awake()
    {
        controls = new TouchAction();
        cameraTransform = Camera.main.transform;
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void Start()
    {
        controls.Touch.SecondaryTouchContact.started += _ => ZoomStart();
        controls.Touch.SecondaryTouchContact.canceled += _ => ZoomEnd();
    }
    private void ZoomStart()
    {
        zoomCoroutine = StartCoroutine(ZoomDetection());
    }
    private void ZoomEnd()
    {
        StopCoroutine(zoomCoroutine);
    }
    IEnumerator ZoomDetection()
    {
        float previousDistance = 0f, distance = 0f;
        while(true)
        {
            distance = Vector2.Distance(controls.Touch.PrimaryFingerPosition.ReadValue<Vector2>(), controls.Touch.SecondaryFingerPosition.ReadValue<Vector2>());
            //Detection
            //Zoom out
            if(distance > previousDistance)
            {
                Vector3 targetPosition = cameraTransform.position;
                targetPosition.z += 1;
                cameraTransform.position = Vector3.Slerp(cameraTransform.position, targetPosition, Time.deltaTime * cameraSpeed);
            }
            //Zoom in
            else if(distance < previousDistance)
            {
                Vector3 targetPosition = cameraTransform.position;
                targetPosition.z -= 1;
                cameraTransform.position = Vector3.Slerp(cameraTransform.position, targetPosition, Time.deltaTime * cameraSpeed);
            }    
            //Keep track of previous distance for next loop
            previousDistance = distance;
            yield return null;
        }
    }
}
