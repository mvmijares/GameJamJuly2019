using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    public Text scoreText;

    private int uiScore;

    private void Awake()
    {
        uiScore = 0;
    }
    private void Update()
    {
        scoreText.text = uiScore.ToString();
    }

    public void UpdateScoreValue(int score)
    {
        uiScore = score;
    }
}
