using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsInterface : MonoBehaviour
{
    public Text scoreValue;

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("Main Menu");   
    }
}
