using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuInterface : MonoBehaviour
{
    
    public void StartGameButtonPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGameButtonPressed()
    {
        Application.Quit();
    }
}
