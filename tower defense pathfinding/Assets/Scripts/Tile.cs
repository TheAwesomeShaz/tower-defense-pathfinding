using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    // public Material transparentBlue;
    // public Material transparentRed;
    // public Material originalMaterial;


    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            Debug.Log("This tile is placeable: " + isPlaceable);

            if (!isPlaceable)
            {
                Debug.Log("blocked this node cuz it was not placeable");
                gridManager.BlockNode(coordinates);
            }

        }
    }

    void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = false;
        }
    }
}
