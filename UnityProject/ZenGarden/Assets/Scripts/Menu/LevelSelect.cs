using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameState;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    //public GameObject manager;
    public GameObject[] levelButtons;
    GameObject background;
    public Sprite bg1;
    public Sprite bg2;
    public Sprite bg3;
    public Sprite bg4;
    public Sprite bg5;
    public Sprite bg6;
    public Sprite[] spriteArray;

    private void Start()
    {
        //manager = GameObject.Find("GameManager");
        GameManager.Instance.GetScenesCompleted();
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        background = GameObject.FindGameObjectWithTag("BackgroundImage");
        spriteArray = new Sprite[6];
        spriteArray[0] = bg1;
        spriteArray[1] = bg2;
        spriteArray[2] = bg3;
        spriteArray[3] = bg4;
        spriteArray[4] = bg5;
        spriteArray[5] = bg6;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetActive(false);
        }
        levelButtons[0].SetActive(true);
        updateBackground();
        checkProgress();
    }

    void updateBackground()
    {
        int count = System.Math.Min(GameManager.Instance.LevelsDone, spriteArray.Length);
        background.GetComponent<Image>().sprite = spriteArray[count-1];
    }

    void checkProgress()
    {
        foreach (var button in levelButtons) button.SetActive(true);
        //for(int i = 0; i < GameManager.Instance.LevelsDone - 1; i++)
        //{
        //    levelButtons[i].SetActive(true);
        //}
    }

    public void PressLevel1()
    {
        GameManager.Instance.LoadNewUnityScene("Level1");
    }

    public void PressLevel2()
    {
        GameManager.Instance.LoadNewUnityScene("Level2");
    }

    public void PressLevel3()
    {
        GameManager.Instance.LoadNewUnityScene("Level3");
    }

    public void PressLevel4()
    {
        GameManager.Instance.LoadNewUnityScene("Level4");
    }

    public void PressLevel5()
    {
        GameManager.Instance.LoadNewUnityScene("Level5");
    }

}
