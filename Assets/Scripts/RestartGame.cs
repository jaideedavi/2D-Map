using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Map_v1");
    }

     void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
