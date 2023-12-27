using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Camera cam;
    private float xRotation = 0f;


    [SerializeField] private float xSensitivity = 30f;
    [SerializeField] private float ySensitivity = 30f;

    [SerializeField] private List<GameObject> UIPanels;
    public List<GameObject> UiPanels => UIPanels;



    private void Awake()
    {
        cam = Camera.main;
    }

    public void ProcessLook(Vector2 input)
    {
        foreach (GameObject panel in UIPanels)
        {
            if (panel.activeInHierarchy)
            {
                return;
            }
        }

        float mouseX = input.x;
        float mouseY = input.y;

        //Calculate camera rotation

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //Apply
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //Rotate player
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);

    }
}
