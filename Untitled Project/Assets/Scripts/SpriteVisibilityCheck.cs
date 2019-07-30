using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteVisibilityCheck : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rooftop roofTop;
    public bool isInvisible;

    Camera cam;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        roofTop = GetComponentInParent<Rooftop>();
        cam = Camera.main;
        
        isInvisible = false;
    }

    private void Update()
    {
        Vector3 viewportPosition = cam.WorldToViewportPoint(transform.position);
        Vector3 spriteCheckPoint = new Vector3(transform.position.x + spriteRenderer.bounds.size.x, transform.position.y, 0);
        Vector3 spriteViewPortPosition = cam.WorldToViewportPoint(spriteCheckPoint);

        if(spriteCheckPoint.x < viewportPosition.x)
        {
            isInvisible = true;
        }
        else
        {
            isInvisible = false;
        }
    }
}
