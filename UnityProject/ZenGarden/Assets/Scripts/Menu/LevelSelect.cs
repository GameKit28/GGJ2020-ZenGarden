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
        //GameManager gameManager = manager.GetComponent<GameManager>();
        GameManager.Instance.GetScenesCompleted();
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
    }

    private void Update()
    {
        
    }

    void checkProgress()
    {
        //for(int i = 0; i < manager.GetCompon;
    }

}
