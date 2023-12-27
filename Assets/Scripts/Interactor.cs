using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevel;

public class Interactor : MonoBehaviour
{
    private Camera cam;
    private Ray ray;

    [SerializeField] private float distance = 6f;
    [SerializeField] private LayerMask mask;



    public bool IsInteracting { get; private set; }

    void Awake()
    {
        cam = Camera.main;

    }
    private void Update()
    {
//        Debug.Log("New Input System");
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartInteraction();
        }

        ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
    }
    public void StartInteraction()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            var interactable = hitInfo.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(this, out bool interactSuccessfull);
                IsInteracting = true;
            }
        }
    }
    void EndInteraction()
    {
        IsInteracting = false;
    }
}

