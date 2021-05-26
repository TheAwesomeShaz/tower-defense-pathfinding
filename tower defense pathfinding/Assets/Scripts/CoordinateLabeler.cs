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
        SetLabelColor();
        ToggleLabels();
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
        Debug.Log(coordinates);

        if (node == null) { Debug.Log("node is null"); return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
            Debug.Log("set to blocked color");
        }
        else if (node.isPath)
        {
            label.color = pathColor;
            Debug.Log("set to path color");
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
            Debug.Log("set to explored color");
        }
        else
        {
            label.color = defaultColor;
            Debug.Log("set to default color");
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
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);


        //due to some reason this label text was getting multiplied by 40 so I divided it by 40 idk what will happen in the future it may break something so I am writing this bigass comment here
        label.text = "(" + (coordinates.x / 40).ToString() + "," + (coordinates.y / 40).ToString() + ")";

        coordinates.x /= 40;
        coordinates.y /= 40;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

}
