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
    private bool levelComplete = false;

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
        if (!levelComplete && Array.TrueForAll(tools, tool => !tool.gameObject.activeInHierarchy || tool.count==0))
        {
            levelComplete = true;
            EndLevel();
        }
        
    }

    public void EndLevel()
    {
        endLevelSplash.SetActive(true);
        //GameManager.Instance.PlaySoundClip("achievement_unlock"); //For some reason this is causing horrible crackling sounds
        Invoke("GoToNextScene", 3);
    }

    public void GoToNextScene()
    {
        if (!GameManager.Instance.IsCurrentLevelCompleted())
        {

            GameManager.Instance.MarkCurrentLevelCompleted();
            GameManager.Instance.LoadNewUnityScene("Story");
        }
        else
        {
            GameManager.Instance.LoadNewUnityScene("LevelSelect");
        }
    }

    internal void SetToolSelected(Tool tool)
    {
        Debug.Log("Selecting tool " + tool.name);
        puzzleController.tool = tool;
    }
}
