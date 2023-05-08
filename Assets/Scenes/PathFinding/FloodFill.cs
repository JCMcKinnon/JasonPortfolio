using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{
    public Map mapObject;
    private Node[,] map;
    public Queue<Node> frontier;
    public List<Vector3> cardinalDirections;
    public List<Node> reached;
    

    public void Start()
    {
        map = mapObject.map;
        cardinalDirections = new List<Vector3>();
        cardinalDirections.Add(Vector2.up);
        cardinalDirections.Add(Vector2.down);
        cardinalDirections.Add(Vector2.left);
        cardinalDirections.Add(Vector2.right);
        frontier = new Queue<Node>();
        reached = new List<Node>();


    }
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            frontier.Clear();
            reached.Clear();
            foreach (var node in map)
            {
                node.examined = false;
                
                node.parent = null;
            }    
            StartFloodFill(map[4, 4]);
            DrawPath(map[4, 4], map[9, 9]);
        }
    }
    public void StartFloodFill(Node start)
    {
        frontier.Enqueue(start);
       while (frontier.Count > 0)
       {

            var currentFront = frontier.Dequeue();
            
            var neighbours = GetNeighbours(currentFront);
            foreach (var neighbour in neighbours)
            {
                var currentNode = neighbour;
                
                if (currentNode.walkable)
                {
                    if (!reached.Contains(currentNode))
                    {
                        frontier.Enqueue(currentNode);
                        currentNode.parent = currentFront;
                        reached.Add(currentNode);
                    }

                }
            }
       }
    }

    public void DrawPath(Node from, Node to)
    {
        while (to != from)
        {
            to.parent.examined = true;
            to = to.parent;
        }               
    }

    public List<Node> GetNeighbours(Node curr)
    {
        //for every direction
        //the position of the node plus a direction exists, add it to the list
        List<Node> neighbours = new List<Node>();

        foreach (var dir in cardinalDirections)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y].position == dir + curr.position)
                    {
                        neighbours.Add(map[x, y]);
                    }
                }
            }
        }
        return neighbours;
    }
}
