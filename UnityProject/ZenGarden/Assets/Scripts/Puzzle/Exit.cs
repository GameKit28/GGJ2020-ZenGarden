using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameState;

public class Exit : MonoBehaviour
{
    public void OnButtonClick()
    {
        GameManager.Instance.LoadNewUnityScene("LevelSelect");
    }
}
