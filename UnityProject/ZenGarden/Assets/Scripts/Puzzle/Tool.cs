using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tool : MonoBehaviour
{
    public TileBase tile;
    public int count;

    private InventoryController inventoryController;

    public void Start()
    {
       inventoryController = gameObject.GetComponentInParent<InventoryController>();
    }

    public void OnButtonClicked()
    {
        inventoryController.SetToolSelected(this);
    }
}
