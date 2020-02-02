using System.Collections;
using System.Collections.Generic;
using GameState;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.Instance.LoadNewUnityScene("LevelSelect");
    }
}
