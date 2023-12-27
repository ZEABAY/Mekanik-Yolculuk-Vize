using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
public class UIManager : MonoBehaviour
{

    public BuildingPanelUI buildPanel;
    public DynamicInventoryDisplay playerBackpack;
    public DynamicInventoryDisplay chestInventory;
    public PlayerInventoryHolder playerHolder;



    private void Start()
    {
        buildPanel.gameObject.SetActive(false);

        SetMouseCursorState(false);
    }


    private void Update()
    {

    }


    public void BuildPanelUI()
    {
        GameObject hand = GameObject.Find("Hand");
        if (hand == null)
        {
            Debug.Log("Hand obj is null ");
            return;
        }

        if (hand.GetComponentInChildren<BuildTool>())
        {

            buildPanel.gameObject.SetActive(!buildPanel.gameObject.activeInHierarchy);
            if (buildPanel.gameObject.activeInHierarchy)
            {
                buildPanel.PopulateButton();
            }

            SetMouseCursorState(buildPanel.gameObject.activeInHierarchy);

        }
    }



    public void Escape()
    {
        if (buildPanel.gameObject.activeInHierarchy)
        {
            BuildPanelUI();
        }

        if (playerBackpack.gameObject.activeInHierarchy)
        {
            DisplayBackpack();
        }

        if (chestInventory.gameObject.activeInHierarchy)
        {
            chestInventory.gameObject.SetActive(false);
            SetMouseCursorState(false);
        }
    }

    public void SetMouseCursorState(bool newState)
    {
        Cursor.visible = newState;
        Cursor.lockState = newState ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    public void DisplayBackpack()
    {
        if (!playerBackpack.gameObject.activeInHierarchy)
        {
            PlayerInventoryHolder.OnPlayerBackpackDisplayRequested?.Invoke(playerHolder.SecondaryInventorySystem);
        }
        else
        {
            playerBackpack.gameObject.SetActive(false);
        }

        SetMouseCursorState(playerBackpack.gameObject.activeInHierarchy);

    }
}
