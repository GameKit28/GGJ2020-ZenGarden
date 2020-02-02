using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameState;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    //public GameObject manager;
    public GameObject[] levelButtons;

    private void Start()
    {
        //manager = GameObject.Find("GameManager");
        GameManager.Instance.GetScenesCompleted();
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        for(int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetActive(false);
        }
        levelButtons[0].SetActive(true);
        checkProgress();
    }

    private void Update()
    {

    }

    void checkProgress()
    {
        for(int i = 0; i < GameManager.Instance.GetScenesCompleted(); i++)
        {
            levelButtons[i+1].SetActive(true);
        }
    }

    public void PressLevel1()
    {
        GameManager.Instance.LoadNewUnityScene("Story");
    }

}
