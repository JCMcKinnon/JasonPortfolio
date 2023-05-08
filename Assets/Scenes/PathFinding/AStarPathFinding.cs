using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AStarPathFinding : MonoBehaviour
{
    //-----init
    public Map mapObject; //set in inspector 
    private Node[,] map;
    private List<Node> openList;
    private List<Node> closedList;
    public GameObject playerTile;
    public delegate List<Node> NeighbourDelegate(Node curr);
    NeighbourDelegate getNeighbours;
    public List<Node> thePath;
    private float timer;
    //----properties



    private List<Vector3> CardinalDirections = new List<Vector3>();

    private void OnEnable()
    {

    }
    private void Start()
    {
        map = new Node[(int)mapObject.mapSize.x,(int)mapObject.mapSize.y];
        map = mapObject.map;
        getNeighbours = GetNeighbours;

        closedList = new List<Node>();
        openList = new List<Node>();
        CardinalDirections.Add(Vector3.up);
        CardinalDirections.Add(new Vector3(1, 1)); //up right
        CardinalDirections.Add(Vector3.right);
        CardinalDirections.Add(new Vector3(1, -1)); //down right
        CardinalDirections.Add(Vector3.down);
        CardinalDirections.Add(new Vector3(-1, -1)); //down left
        CardinalDirections.Add(Vector3.left);
        CardinalDirections.Add(new Vector3(-1, 1)); //up left

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetPath(FindPath(map[4, 0], map[6, 8]));
        }
    }

    public Node FindPath(Node start, Node end)
    {
        //add start to open list
        thePath = new List<Node>();
        openList.Add(start);
        Instantiate(playerTile, new Vector3(start.position.x, start.position.y, -2), Quaternion.identity);
        Instantiate(playerTile, new Vector3(end.position.x, end.position.y, -2), Quaternion.identity);


        while (openList.Count > 0)
        {
            List<Node> neighbours = new List<Node>();

            //current = lowest fcost openlist
            var current = FindNodeWithLowestFCost(openList);
            openList.Remove(current);
            closedList.Add(current);
            if(current == end)
            {
                print("found end");
                

                break;
            }

            neighbours = GetNeighbours(current);
            current.Hcost = current.GetHCost(end);
            current.Gcost = GetDistance(start, current);

            foreach (var neighbour in neighbours)
            {
                if(!neighbour.walkable || closedList.Contains(neighbour))
                {
                    continue;
                }
                neighbour.Hcost = neighbour.GetHCost(end);
                neighbour.Gcost = Vector3.Distance(neighbour.position, start.position);
                float newMovementCostToNeighbour = current.Gcost +  GetDistance(current,neighbour);
                if (newMovementCostToNeighbour < neighbour.Gcost || !openList.Contains(neighbour))
                {
                    neighbour.Gcost = newMovementCostToNeighbour;
                    neighbour.Hcost = neighbour.GetHCost(end);
                    neighbour.parent = current;
                    thePath.Add(current);
                    
                }
                if (!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
            }
        }
        return end;
    }
    public Node FindNodeWithLowestFCost(List<Node> nodeList)
    {
        foreach (var node in nodeList)
        {
            
            node.GetFCost();
        }
        var min = nodeList.Min(x => x.FCost);
        Node output = nodeList[0];
        foreach (var node in nodeList)
        {
            if(node.FCost == min)
            {
                output = node;
            }
        }
        return output;
    }
    public List<Node> GetNeighbours(Node curr)
    {
        //for every direction
        //the position of the node plus a direction exists, add it to the list
        List<Node> neighbours = new List<Node>();

        foreach(var dir in CardinalDirections)
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
    float GetDistance(Node a, Node b)
    {
        //get the difference between the two with Vector3.Distance
        //take the (abs)lowest of the numbers x or y
        //move diagnoally by that number
        //subtract the lowest number from the higher number
        //and thats how much you move in that direction;

        float dstX = Mathf.Abs(a.position.x - b.position.x);
        float dstY = Mathf.Abs(a.position.y - b.position.y);

        if(dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstX - dstY);


    }
    public void SetPath(Node node)
    {     
        if(node.parent == null)
        {
            return;
        }
        Instantiate(playerTile, node.position, Quaternion.identity);
        SetPath(node.parent);
    }

}
