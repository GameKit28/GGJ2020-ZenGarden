using System.Collections;
using System.Collections.Generic;
using GameState;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        GameManager.Instance.PlaySoundClip("background_music");
    }
    
    public void PlayGame()
    {
        GameManager.Instance.LoadNewUnityScene("Story");
    }
}
