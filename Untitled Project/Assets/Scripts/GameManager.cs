using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _isntance;
    public static GameManager instance { get { return _isntance; } }

    private UserInterfaceManager userInterfaceManager;
    private CreditsInterface creditsInterface;
    private Player player;
    public Rooftop[] roofTops;
    public float speed;
    public int score;
    private int _lastScore;
    public int lastScore { get { return _lastScore; } }
    private float currentTimer;

    public GameObject largeCrate;
    public GameObject smallCrate;

    Dictionary<Rooftop, Vector2> roofTopOrigins;


    public float gameStartTime;
    private float currentStartTime;
    private bool initializeCondition;
    Scene currScene;

    public float deathTimer;
    private float currentDeathTimer;

    //Initialize method
    private void Awake()
    {
        InitializeSingleton();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void InitializeSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _isntance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currScene = scene;
        switch (scene.name)
        {
            case "Game":
                {
                    InitializeGame();
                    break;
                }
            case "Credits":
                {
                    InitializeCredits();
                    break;
                }
        }
    }

    private void InitializeGame()
    {
        userInterfaceManager = FindObjectOfType<UserInterfaceManager>();

        player = FindObjectOfType<Player>();

        roofTopOrigins = new Dictionary<Rooftop, Vector2>();

        foreach (Rooftop rooftop in roofTops)
        {
            roofTopOrigins.Add(rooftop, rooftop.transform.position);
        }

        currentStartTime = 0.0f;
        initializeCondition = false;
    }
    private void InitializeCredits()
    {
        creditsInterface = FindObjectOfType<CreditsInterface>();
        creditsInterface.scoreValue.text = _lastScore.ToString();
    }
    // Update function
    private void Update()
    {
        switch (currScene.name)
        {
            case "Game":
                {
                    currentStartTime += Time.deltaTime;
                    if (currentStartTime > gameStartTime)
                    {
                        if (!initializeCondition)
                        {
                            InitializeIdleScore();
                            initializeCondition = true;
                        }
                        HandleGameTasks();
                    }
                    HandleUserInterface();
                    if (Input.GetKey(KeyCode.R))
                    {
                        OnResetGameEventCalled();
                    }
                    break;
                }
            case "Credits":
                {
                    
                    break;
                }
        }
    
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Credits");
        }
    }
    private void HandleGameTasks()
    {
        if (player.isDead)
        {
          
            _lastScore = score;

            if(IsInvoking("CalculateIdleScore"))
                CancelInvoke("CalculateIdleScore");

            currentDeathTimer += Time.deltaTime;
            if (currentDeathTimer >= deathTimer)
            {
                currentDeathTimer = 0;
                OnResetGameEventCalled();
            }
        }
        else
        {
            MoveTiles();

            roofTops[0].CheckRooftopSpriteEvent();
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
    private void HandleUserInterface()
    {
        userInterfaceManager.UpdateScoreValue(score);
    }
    private void InitializeIdleScore()
    {
        InvokeRepeating("CalculateIdleScore", 1f, 1f);
    }
    private void OnResetGameEventCalled()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach(Enemy e in enemies)
        {
            Destroy(e.gameObject);
        }
        CancelInvoke("CalculateIdleScore");
        ResetRoofTopPositions();
        player.OnPlayerResetEvent();

        score = 0;
        currentStartTime = 0.0f;
        initializeCondition = false;
    }

    private void ResetRoofTopPositions()
    {
        int i = 0;
        foreach (KeyValuePair<Rooftop, Vector2> pair in roofTopOrigins)
        {
            pair.Key.transform.position = pair.Value;
            pair.Key.DeleteObstacles();
            pair.Key.ResetRooftop();
            roofTops[i] = pair.Key;
            i++;
        }
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
