using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool isInventoryOpen = false;
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject inventoryUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(isInventoryOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }


    void OpenInventory()
    {
        inventoryUI.SetActive(true);
        isInventoryOpen = true;
    }

    void CloseInventory()
    {
        inventoryUI.SetActive(false);
        isInventoryOpen = false;
    }

}
