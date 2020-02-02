using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    PuzzleController puzzleController;
    // Start is called before the first frame update
    void Start()
    {
        puzzleController = GameObject.FindGameObjectWithTag("PuzzleController").GetComponent<PuzzleController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SetToolSelected(Tool tool)
    {
        Debug.Log("Selecting tool " + tool.name);
        puzzleController.tool = tool;
    }
}
