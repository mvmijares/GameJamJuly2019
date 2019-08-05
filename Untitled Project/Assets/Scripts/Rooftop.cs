using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RooftopType
{
    Large, Small
}
public class Rooftop : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private BoxCollider2D boxCollider2D;

    [SerializeField] private List<SpriteVisibilityCheck> sprites;
    public RooftopType type;
    public List<Transform> obstacleLocations;
    Dictionary<Transform, GameObject> obstacleOrigins;
    public GameObject obstacleContainer;
    //Intialization method
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();

        obstacleLocations = new List<Transform>();
        Transform children = obstacleContainer.GetComponentInChildren<Transform>();

        obstacleOrigins = new Dictionary<Transform, GameObject>();
        foreach(Transform child in children)
        {
            if(child != obstacleContainer.transform)
            {
                if (child.gameObject.tag == "Obstacle")
                {
                    obstacleLocations.Add(child);

                    if(child.childCount > 0)
                    {
                        GameObject clone = Instantiate(child.GetChild(0).gameObject);
                        clone.SetActive(false);
                        obstacleOrigins.Add(child, clone);
                    }
                }
            }
        }

        sprites = new List<SpriteVisibilityCheck>();
        SpriteVisibilityCheck[] spriteArray = GetComponentsInChildren<SpriteVisibilityCheck>();

        foreach(SpriteVisibilityCheck sprite in spriteArray)
        {
            sprites.Add(sprite);
        }
    }
    public void CheckRooftopSpriteEvent()
    {
        if (CheckSpritesVisibility())
        {
            gameManager.MoveRooftop(this);
            DeleteObstacles();
            InitializeObstacles();
        }
    }
    public void ResetRooftop()
    {
        foreach(KeyValuePair<Transform, GameObject> pair in obstacleOrigins)
        {
            GameObject clone = Instantiate(pair.Value);

            clone.transform.SetParent(pair.Key);
            clone.transform.localScale = Vector3.one * 2;
            clone.transform.localPosition = new Vector3(0, -0.5f, 0);

            clone.SetActive(true);
        }
    }
    private bool CheckSpritesVisibility()
    {
        foreach(SpriteVisibilityCheck sprite in sprites)
        {
            if (!sprite.isInvisible)
                return false;
        }
        return true;
    }
    /// <summary>
    /// Method for deleting obstacles once rooftop goes out of screen
    /// </summary>
    public void DeleteObstacles()
    {
        foreach(Transform child in obstacleLocations)
        {
            if (child.childCount > 0)
            {
                Destroy(child.GetChild(0).gameObject);
            }
        }
    }

    public void InitializeObstacles()
    {
        if (obstacleLocations.Count > 1)
        {
            int obstacles = UnityEngine.Random.Range(1, 4);

            for (int i = 0; i < obstacles; i++)
            {
                GameObject obstacleRef = GetObstacle();

                GameObject clone = Instantiate(obstacleRef);

                clone.transform.SetParent(obstacleLocations[i]);
                clone.transform.localPosition = new Vector3(0, -0.5f, 0);
            }
        }
        else
        {
            GameObject obstacleRef = GetObstacle();

            GameObject clone = Instantiate(obstacleRef);

            clone.transform.SetParent(obstacleLocations[0]);
            clone.transform.localPosition = new Vector3(0, -0.5f, 0);
        }
    }


    public GameObject GetObstacle()
    {
        int choice = UnityEngine.Random.Range(1, 2);

        if (choice == 1)
            return gameManager.largeCrate;
        else
            return gameManager.smallCrate;
    }
}
