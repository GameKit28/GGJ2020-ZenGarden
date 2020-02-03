using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameState;

public class InventoryController : MonoBehaviour
{
    PuzzleController puzzleController;
    Tool[] tools;
    private GameObject endLevelSplash;
    public bool goToNextScene = true;

    // Start is called before the first frame update
    void Start()
    {
        puzzleController = GameObject.FindGameObjectWithTag("PuzzleController").GetComponent<PuzzleController>();
        tools = gameObject.transform.GetComponentsInChildren<Tool>();

        endLevelSplash = GameObject.FindGameObjectWithTag("EndLevelSplash");
        endLevelSplash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Array.TrueForAll(tools, tool => !tool.gameObject.activeInHierarchy || tool.count==0))
        {
            EndLevel();
        }
        
    }

    public void EndLevel()
    {
        endLevelSplash.SetActive(true);
        GameManager.Instance.LevelsDone++;
        Invoke("GoToNextScene", 3);
    }

    public void GoToNextScene()
    {
        GameManager.Instance.LoadNewUnityScene("Story");
    }

    internal void SetToolSelected(Tool tool)
    {
        Debug.Log("Selecting tool " + tool.name);
        puzzleController.tool = tool;
    }
}
