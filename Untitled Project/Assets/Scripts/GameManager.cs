using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private UserInterfaceManager userInterfaceManager;
    private Player player;
    public Rooftop[] roofTops;
    public float speed;
    public int score;

    private float currentTimer;

    public GameObject largeCrate;
    public GameObject smallCrate;

    Dictionary<Rooftop, Vector2> roofTopOrigins;
    //Initialize method
    private void Awake()
    {
        userInterfaceManager = FindObjectOfType<UserInterfaceManager>();

        player = FindObjectOfType<Player>();

        roofTopOrigins = new Dictionary<Rooftop, Vector2>();

        foreach(Rooftop rooftop in roofTops)
        {
            roofTopOrigins.Add(rooftop, rooftop.transform.position);
        }
    }

    private void InitializeIdleScore()
    {
        InvokeRepeating("CalculateIdleScore", 1f, 1f);
    }
    // Update function
    private void Update()
    {
        if (player.isDead)
        {
            CancelInvoke("CalculateIdleScore");
        }
        else
        {
            MoveTiles();

            roofTops[0].CheckRooftopSpriteEvent();
        }
        if (Input.GetKey(KeyCode.R))
        {
            OnResetGameEventCalled();
        }

        userInterfaceManager.UpdateScoreValue(score);
    }

    private void OnResetGameEventCalled()
    {
        player.OnPlayerResetEvent();
        foreach(KeyValuePair<Rooftop, Vector2> pair in roofTopOrigins)
        {
            pair.Key.transform.position = pair.Value;
            pair.Key.DeleteObstacles();
            pair.Key.ResetRooftop();
        }
    }
    private void CalculateIdleScore()
    {
        score += 100;
    }
    public void AddScore(int value)
    {
        score += value;
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
        Rooftop reference1, reference2, reference3;

        reference1 = roofTops[0];
        reference2 = roofTops[1];
        reference3 = roofTops[2];

        roofTops[0] = reference2;
        roofTops[1] = reference3;
        roofTops[2] = reference1;

    
        if (roofTops[1].type == RooftopType.Large && roofTops[2].type == RooftopType.Large)
        {
            roofTops[2].transform.position = new Vector2(roofTops[1].transform.position.x + 40f, roofTops[2].transform.position.y);
        }
        else
        {
            roofTops[2].transform.position = new Vector2(roofTops[1].transform.position.x + 34f, roofTops[2].transform.position.y);
        }
    }
}
