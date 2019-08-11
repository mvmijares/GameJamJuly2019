using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager _gameManager;
    public float speed;
    public int scoreValue;
    public GameObject deathAnimation;

    private int enemyLayerMask;
    private int obstacleLayerMask;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

    
    }
    public void InitializeEnemy()
    {
        enemyLayerMask = LayerMask.NameToLayer("Ninja");
        obstacleLayerMask = LayerMask.NameToLayer("Obstacle");
        Physics2D.IgnoreLayerCollision(enemyLayerMask, obstacleLayerMask);
    }
    private void Update()
    {
        HandleEnemyMovement();  
    }

    private void HandleEnemyMovement()
    {
        transform.position -= Vector3.right * speed * Time.deltaTime;
    }

    public void HandleEnemyDeath()
    {
        GameObject clone = Instantiate(deathAnimation, transform.position, Quaternion.identity);
        if (_gameManager)
            _gameManager.AddScore(scoreValue);

        Destroy(this.gameObject);
    }
}
