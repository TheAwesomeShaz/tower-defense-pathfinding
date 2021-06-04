using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();


    }


    // Update is called once per frame
    void Update()
    {
        ToggleLabels();
        SetLabelColor();
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

    }

    private void SetLabelColor()
    {
        if (gridManager == null)
        {
            Debug.Log("grid manager was null");
            return;
        }

        Node node = gridManager.GetNode(coordinates);


        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
            Debug.Log("Set this node to blocked color cuz isWalkable: " + node.isWalkable);
        }
        else if (node.isPath)
        {
            label.color = pathColor;
            //Debug.Log("Set this node to path color since this is the path");


        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
            Debug.Log("Set this node to explored color cuz we have searched through this");
        }
        else
        {
            label.color = defaultColor;

        }

    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCoordinates()
    {
        if (gridManager == null) { return; }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);


        //due to some reason this label text was getting multiplied by 40 so I divided it by 40 idk what will happen in the future it may break something so I am writing this bigass comment here
        label.text = "(" + (coordinates.x).ToString() + "," + (coordinates.y).ToString() + ")";


        coordinates.x /= 40;
        coordinates.y /= 40;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

}
