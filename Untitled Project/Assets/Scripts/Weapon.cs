    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {

        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ninja"))
        {

        }
    }
}
