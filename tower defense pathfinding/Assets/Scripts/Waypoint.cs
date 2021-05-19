using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    // public Material transparentBlue;
    // public Material transparentRed;
    // public Material originalMaterial;


    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }




    void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = false;
        }
    }
}
