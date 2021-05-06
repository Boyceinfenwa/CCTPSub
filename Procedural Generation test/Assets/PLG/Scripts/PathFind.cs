using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind : MonoBehaviour
{
    Grid grid;
    public Transform start, end;
    GameObject startobj, endobj;
    void Awake()
    {
        grid = GetComponent<Grid>();

        startobj = GameObject.FindGameObjectWithTag("Start");
        endobj = GameObject.FindGameObjectWithTag("Finish");
        start = startobj.transform;
        end = endobj.transform;

    }

    void Update()
    { 
        FindPath(start.position, end.position);
    }

    void FindPath(Vector3 startPos, Vector3 endpos)
    {
        Node startNode = grid.nodefromWorldPoint(startPos);
        Node endNode = grid.nodefromWorldPoint(endpos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fcost < node.fcost || openSet[i].fcost == node.fcost)
                {
                    if (openSet[i].hcost < node.hcost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == endNode)
            {
                RetracePath(startNode, endNode);
                print("Path to exit found sucessfully");
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gcost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gcost || !openSet.Contains(neighbour))
                {
                    neighbour.gcost = newCostToNeighbour;
                    neighbour.hcost = GetDistance(neighbour, endNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
