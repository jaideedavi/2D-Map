using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "Map_v1";

    public void Restart()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Map_v1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
