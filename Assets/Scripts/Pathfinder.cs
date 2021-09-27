using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    //gCost steps by 1 each node, h -> distance from node to end, f -> g + h, set parent to new node;
    GridGen gridGen;

    List<Node> openSet;
    List<Node> closedSet;

    private void Start()
    {
        gridGen = GetComponent<GridGen>();
    }
    public List<Node> FindPath(Vector3Int startPosition, Vector3Int endPosition)
    {
        Node startNode = new Node(startPosition, gridGen.grid[startPosition.x, startPosition.y]);
        Node endNode = new Node(endPosition, gridGen.grid[endPosition.x, endPosition.y]);
        openSet = new List<Node>();
        closedSet = new List<Node>();
        startNode.gCost = 0;
        startNode.hCost = GetDistance(startNode, endNode);
        //put starting node in OPEN list
        openSet.Add(startNode);
        //while the open list is not empty
        while(openSet.Count > 0)
        {
            //take from the open list the node with the lowest f
            Node currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost)
                {
                    currentNode = openSet[i];
                }
                else if(openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
                
            }
            ////////////////////////////////////////////////
            // if current node is the target we found the solution (maybe compared positions?)
            if(currentNode.worldPosition == endPosition)
            {
                return RetracePath(startNode, currentNode);
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            //Generate each neighbour node that comes after current node
            // for each neighbor of current node
            foreach (Node neighbour in GetNeighbours(currentNode, endNode))
            {
                if (neighbour.space != GridGen.gridSpace.floor || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        return new List<Node>();
    }

    public List<Node> GetNeighbours(Node node, Node endNode)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if ((x == 0 && y == 0) || (x == -1 && y != 0) || (x == 1 && y != 0))
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridGen.roomSizeWorldUnits.x && checkY >= 0 && checkY < gridGen.roomSizeWorldUnits.x)
                {
                    Node neighbor = new Node(new Vector3Int(checkX, checkY, 0), gridGen.grid[checkX, checkY]);
                    neighbor.hCost = GetDistance(neighbor, endNode);
                    neighbours.Add(neighbor);
                }
            }
        }

        return neighbours;
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;

        }
        path.Reverse();

        return path;
        //Do something with path;
    }
 
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return (dstX + dstY);
    }

}
