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

    public GameObject obstacleContainer;
    //Intialization method
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();

        obstacleLocations = new List<Transform>();
        Transform children = obstacleContainer.GetComponentInChildren<Transform>();

        foreach(Transform child in children)
        {
            if(child != obstacleContainer.transform)
            {
                if (child.gameObject.tag == "Obstacle")
                    obstacleLocations.Add(child);

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
                Destroy(child.GetChild(0));
            }
        }
    }
}
