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

    //Intialization method
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnBecameInvisible()
    {
        gameManager.MoveRooftop(this);
    }
}
