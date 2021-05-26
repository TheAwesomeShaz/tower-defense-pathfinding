using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    //frontier is basically the explored nodes when we are at the current node kinda like explored fog of war in age of empires 
    Queue<Node> frontier = new Queue<Node>();

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        startNode = gridManager.Grid[startCoordinates];
        destinationNode = gridManager.Grid[destinationCoordinates];

        BreadthFirstSearch();
        BuildPath();
    }

    void ExploreNeighbours()
    {
        List<Node> neighbours = new List<Node>();
        foreach (Vector2Int direction in directions)
        {
            // if we are at node (1,0) we can get right neighbour by 
            // currentNode coordinates + direction = right node coordinates i.e
            // (1,0)                   + (1,0)     = (2,0)

            // the direction for up   down   left   right will be
            // in (x,y) format: (0,1) (0,-1) (-1,0) (1,0)  respectively
            // (since it is Vector2Int direction so it stores two values)

            Vector2Int neighbourCoords = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighbourCoords))
            {
                neighbours.Add(grid[neighbourCoords]);
            }
        }

        foreach (Node neighbour in neighbours)
        {
            if (!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                neighbour.connectedTo = currentSearchNode;
                reached.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }

    void BreadthFirstSearch()
    {
        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbours();
            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currNode = destinationNode;

        path.Add(currNode);
        currNode.isPath = true;

        // connected to is basically like a pointer which points to the connected element!
        while (currNode.connectedTo != null)
        {
            // traversing the nodes back from destination to start through the "connectedTo" pointers
            // (curr = curr.next type stuff)
            currNode = currNode.connectedTo;

            //Add the node in the path list
            path.Add(currNode);

            // set isPath variable of Node class instance to true
            currNode.isPath = true;
        }

        // now that we have the list which goes from the destination to the start we will now reverse the list 
        // so that it goes from start to destination 
        path.Reverse();
        //and then return the path so that It can be followed by something
        return path;
    }

}
