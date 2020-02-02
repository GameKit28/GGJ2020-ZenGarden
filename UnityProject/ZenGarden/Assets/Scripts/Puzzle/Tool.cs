using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Tool : MonoBehaviour
{
    public TileBase tile;
    public Sprite image;
    public int count;

    public Text countText;
    private InventoryController inventoryController;
    

    public void Start()
    {
       inventoryController = gameObject.GetComponentInParent<InventoryController>();
    
    }

    public void Update()
    {
        countText.text = "x " + count;
    }

    public void OnButtonClicked()
    {
        inventoryController.SetToolSelected(this);
    }
}
