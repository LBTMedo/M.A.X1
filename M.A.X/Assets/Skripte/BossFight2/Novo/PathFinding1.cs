﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding1 : MonoBehaviour {

    Grid1 grid;

    public Transform seeker, target;
    EnemyMovement movement;

    private void Awake()
    {
        grid = GetComponent<Grid1>();
        movement = FindObjectOfType<EnemyMovement>();
    }

    private void Start()
    {
        StartCoroutine(IsciPot());
    }

    IEnumerator IsciPot()
    {
        while (true)
        {
            FindPath(seeker.position, target.position);
            yield return new WaitForSeconds(0.6f);
        }
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node1 startNode = grid.NodeFromWorldPoint(startPos);
        Node1 targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node1> openSet = new List<Node1>();
        HashSet<Node1> closedSet = new HashSet<Node1>();

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node1 currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode)
            {
                movement.UpdatePath(RetracePath(startNode, targetNode));
                return;
            }

            foreach(Node1 neighbour in grid.GetNeighbours(currentNode))
            {
                if(!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    Vector2[] RetracePath(Node1 startNode, Node1 endNode)
    {
        List<Node1> path = new List<Node1>();
        Node1 currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        foreach(Node1 n in path)
        {
            Debug.Log(n.worldPosition);
        }

        grid.path = path;
        Vector2[] _path = new Vector2[path.Count];

        for (int i = 0; i < path.Count; i++)
        {
            _path[i] = path[i].worldPosition;
        }

        return _path;
    }

    int GetDistance(Node1 nodeA, Node1 nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
