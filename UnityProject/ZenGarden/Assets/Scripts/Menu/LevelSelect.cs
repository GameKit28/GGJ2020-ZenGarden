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
   
    public Sprite[] spriteArray;

    private void Start()
    {
        //manager = GameObject.Find("GameManager");
        GameManager.Instance.GetScenesCompleted();
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        background = GameObject.FindGameObjectWithTag("BackgroundImage");
        
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetActive(false);
        }
        levelButtons[0].SetActive(true);
        updateBackground();
        checkProgress();
        
        GameManager.Instance.PlaySoundClip("enter_game");
    }

    void updateBackground()
    {
        int count = System.Math.Max(1, System.Math.Min(GameManager.Instance.LevelsDone, spriteArray.Length));
        background.GetComponent<Image>().sprite = spriteArray[count-1];
    }

    void checkProgress()
    {
        var activateCount = System.Math.Min(GameManager.Instance.LevelsDone, levelButtons.Length);
        for (int i = 0; i < activateCount; i++)
        {
            levelButtons[i].SetActive(true);
        }
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
