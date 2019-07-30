using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private UserInterfaceManager userInterfaceManager;
    public Rooftop[] roofTops;
    public float speed;
    public int score;

    private float currentTimer;
    
    //Initialize method
    private void Awake()
    {
        userInterfaceManager = FindObjectOfType<UserInterfaceManager>();
        InvokeRepeating("CalculateIdleScore", 1f, 1f);
    }
    // Update function
    private void Update()
    {
        MoveTiles();
 
    }

    private void CalculateIdleScore()
    {
        score += 100;
        userInterfaceManager.UpdateScoreValue(score);
    }

    /// <summary>
    /// Method to move roof top to left based on speed.
    /// </summary>
    private void MoveTiles()
    {
        foreach(Rooftop t in roofTops)
        {
            t.gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    public void MoveRooftop(Rooftop roofTop)
    {
        int roofTopIndex = Array.IndexOf(roofTops, roofTop);
        Rooftop reference = roofTops[roofTopIndex];

        roofTops[0] = roofTops[1];
        roofTops[1] = roofTops[2];
        roofTops[2] = reference;

    
        if (roofTops[1].type == RooftopType.Large && reference.type == RooftopType.Large)
        {
            reference.transform.position = new Vector2(roofTops[1].transform.position.x + 40f, reference.transform.position.y);
        }
        else
        {
            reference.transform.position = new Vector2(roofTops[1].transform.position.x + 34f, reference.transform.position.y);
        }
    }
}
