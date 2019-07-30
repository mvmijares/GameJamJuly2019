using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// An enum to store spacing type between rooftops
///     - Between 2 small rooftops
///     - Between 2 large rooftops
///     - Lastly, between a large and small rooftop ( Works vice versa )
/// </summary>
public enum SpacingType
{
    Small, Large, Different
}
/// <summary>
/// struct to contain information on rooftop spacing
/// </summary>
public struct RooftopSpacing
{
    public SpacingType type;
    public float distance;
}
public class GameManager : MonoBehaviour
{
    public Rooftop[] roofTops;
    public float speed;

    private RooftopSpacing[] spacingType;

    //Initialize method
    private void Awake()
    {

    }

    private void CreateSpacing()
    {
        spacingType = new RooftopSpacing[3];
        spacingType[0].type = SpacingType.Large;
        spacingType[0].distance = 31f;
        spacingType[1].type = SpacingType.Small;
        spacingType[1].distance = 21f;
        spacingType[2].type = SpacingType.Different;
        spacingType[2].distance = 25f;
    }
    // Update function
    private void Update()
    {
        MoveTiles();
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
        int roofTopIndex = Array.IndexOf(roofTops, roofTop);
        Rooftop reference = roofTops[roofTopIndex];

        roofTops[0] = roofTops[1];
        roofTops[1] = roofTops[2];
        roofTops[2] = reference;

        if(roofTops[1].type == RooftopType.Small && reference.type == RooftopType.Small)
        {
            reference.transform.position = new Vector2(roofTops[1].transform.position.x + 21f, reference.transform.position.y);
        }
        else if (roofTops[1].type == RooftopType.Large && reference.type == RooftopType.Large)
        {
            reference.transform.position = new Vector2(roofTops[1].transform.position.x + 29f, reference.transform.position.y);
        }
        else
        {
            reference.transform.position = new Vector2(roofTops[1].transform.position.x + 25f, reference.transform.position.y);
        }
    }
}
