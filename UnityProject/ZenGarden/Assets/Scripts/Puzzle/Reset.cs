using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public void OnButtonClick()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
