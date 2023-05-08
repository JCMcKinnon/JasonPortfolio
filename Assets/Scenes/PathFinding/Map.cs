using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Map : MonoBehaviour
{
    public Vector2 mapSize;
    public Node[,] map;
    public GameObject emptyTile;
    public GameObject wallTile;
    public GameObject examinedTiled;

    public Material examinedMat;
    public Material emptyMat;
    public Material wallMat;

    public Dictionary<Vector3,GameObject> tiles;

    public Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        map = new Node[(int)mapSize.x, (int)mapSize.y];
        tiles = new Dictionary<Vector3, GameObject>();
        CreateMap();
    }
    private void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Mathf.RoundToInt(Input.mousePosition.x), Mathf.RoundToInt(Input.mousePosition.y),0));
        mousePos.z = 0;
        if (Input.GetKey(KeyCode.Mouse0))
        {            
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (Mathf.RoundToInt(mousePos.x) == map[x, y].position.x && Mathf.RoundToInt(mousePos.y) == map[x, y].position.y)
                    {
                        map[x, y].walkable = false;
                    }
                  
                 
                }
            }
        }


        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (!map[x, y].walkable)
                {

                    foreach (var tile in tiles)
                    {
                        if (tile.Key == map[x, y].position)
                        {
                            tile.Value.GetComponent<Renderer>().material = wallMat;
                        }
                    }

                }
            }
        }


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    /*if (!map[x, y].walkable)
                    {
                        
                            foreach (var tile in tiles)
                            {
                                if (tile.Key == map[x, y].position)
                                {
                                    tile.Value.GetComponent<Renderer>().material = wallMat;
                                }
                            }
                        
                    }*/
                    if (map[x, y].examined)
                    {                       
                            foreach (var tile in tiles)
                            {
                                if (tile.Key == map[x, y].position)
                                {
                                    tile.Value.GetComponent<Renderer>().material = examinedMat;
                                }
                            }

                    }
                    if(!map[x,y].examined && map[x, y].walkable)
                    {
                        foreach (var tile in tiles)
                        {
                            if (tile.Key == map[x, y].position)
                            {
                                tile.Value.GetComponent<Renderer>().material = emptyMat;
                            }
                        }
                    }
                }
            }
        }
        
    }

    public void CreateMap()
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                map[x, y] = new Node(new Vector3(x, y,0),true);
                tiles.Add(map[x,y].position, Instantiate(emptyTile, map[x, y].position, Quaternion.identity));
                
            }
        }
        foreach (var tile in tiles)
        {
           
                tile.Value.GetComponent<Renderer>().material= emptyMat;
            
        }
    }
}
