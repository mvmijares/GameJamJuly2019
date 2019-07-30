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

    public RooftopType type;
    public List<Transform> obstacleLocations;

    //Intialization method
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();

        obstacleLocations = new List<Transform>();
        Transform children = GetComponentInChildren<Transform>();

        foreach(Transform child in children)
        {
            if(child != this.transform)
            {
                if (child.gameObject.tag == "Obstacle")
                    obstacleLocations.Add(child);

            }
        }
    }

    /// <summary>
    /// Method for deleting obstacles once rooftop goes out of screen
    /// </summary>
    public void DeleteObstacles()
    {
        foreach(Transform child in obstacleLocations)
        {
            if(child.GetChild(0) != null)
            {
                Destroy(child.GetChild(0));
            }
        }
    }
    /// <summary>
    /// Method 
    /// </summary>
    private void OnBecameInvisible()
    {
        DeleteObstacles();
        gameManager.MoveRooftop(this);
    }
}
