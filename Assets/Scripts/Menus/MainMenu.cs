using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PlayGame()
    {
        Debug.Log("Start");
        SceneManager.LoadScene(1);
    }
    
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
