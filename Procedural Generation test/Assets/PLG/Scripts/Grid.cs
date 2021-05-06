using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector3 gridWorldSize;
    Vector2 box;
    public float nodRadius;
    Node[,] grid;
    float nodeDiameter;
    int gridX, gridY;
    public List<Node> path;


    void Awake()
    {
        nodeDiameter = nodRadius * 2;
        gridX = Mathf.RoundToInt( gridWorldSize.x / nodeDiameter);
        gridY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        
        CreateGrid();
    }

    private void Update()
    {
        for(int i = 0; i < 25; i++)
        {
            CreateGrid();
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, gridWorldSize);
        if(grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.clear : Color.red;
                if (path != null)
                    if (path.Contains(n))
                    {
                        print("draw");
                        Gizmos.color = Color.green;
                    }
                        
                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
            }
        }

    }

    public void CreateGrid()
    {
        grid = new Node[gridX, gridY];
        Vector3 worldbtmLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y/2;
        for(int x = 0; x< gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 worldPoint = worldbtmLeft + Vector3.right * (x * nodeDiameter + nodRadius) + Vector3.up * (y * nodeDiameter + nodRadius);
                box = new Vector2(nodeDiameter - 0.1f, nodeDiameter - 0.1f);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodRadius,unwalkableMask));
                grid[x,y] = new Node(walkable,worldPoint,x,y);
               
                
            }
        }

        
    }

    public Node nodefromWorldPoint(Vector3 worldPos)
    {
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridX - 1) * percentX);
        int y = Mathf.RoundToInt((gridY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridX && checkY >= 0 && checkY < gridY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }
}
