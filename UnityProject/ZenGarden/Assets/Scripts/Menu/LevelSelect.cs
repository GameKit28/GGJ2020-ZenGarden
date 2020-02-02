using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameState;

public class LevelSelect : MonoBehaviour
{
    public GameObject manager;
    public GameObject[] levelButtons;

    private void Start()
    {
        manager = GameObject.Find("GameManager");
        GameManager.Instance.GetScenesCompleted();
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        for(int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetActive(false);
        }
        checkProgress();
    }

    private void Update()
    {
        
    }

    void checkProgress()
    {
        for(int i = 0; i < GameManager.Instance.GetScenesCompleted(); i++)
        {
            levelButtons[i].SetActive(true);
        }
    }

}
