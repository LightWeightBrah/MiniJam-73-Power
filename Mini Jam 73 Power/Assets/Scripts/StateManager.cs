using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public void Retry()
    {
        AudioManager.instance.PlayButtonSound();

        Time.timeScale = 1f;

        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        AudioManager.instance.PlayButtonSound();

        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
        
    }
}
