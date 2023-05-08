using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 position;
    public bool walkable;
    public bool examined;
    public List<Node> neighbours;
    public bool wallInstantiated;
    public bool examinedInstantiated;
    
    //a*
    public float Gcost;
    public float Hcost;
    public float FCost;
    //a*
    
    //floodfill

    //floodfill
    public Node parent;
    public Node(Vector3 pos, bool walkable)
    {
        this.walkable = walkable;
        position = pos;
    }
    public float GetFCost()
    {
        FCost = Gcost + Hcost;
        return Gcost + Hcost;        
    }
    public void SetNeighbours(List<Node> theNeighbours)
    {
        neighbours = new List<Node>();
        neighbours = theNeighbours;
    }
    public float GetHCost(Node end)
    {
        var dx = Mathf.Abs(position.x - end.position.x);
        var dy = Mathf.Abs(position.y - end.position.y);
        return 3 * Mathf.Sqrt(dx * dx + dy * dy);
    }
}
