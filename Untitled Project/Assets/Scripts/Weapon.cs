    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int playerLayer;
    private int weaponLayer;
    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        weaponLayer = LayerMask.NameToLayer("Weapon");

        ResetPhysicsCollision();

    }
    private void ResetPhysicsCollision()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, weaponLayer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ninja"))
        {
            collision.gameObject.GetComponent<Enemy>().HandleEnemyDeath();
        }
    }
}
