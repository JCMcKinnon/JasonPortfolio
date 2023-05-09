using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] easyObstacles;

    public GameObject[] instantiatedObjects;

    private float timer;
    public GameObject start;
    public GameObject goal;

    public float speed;
    // Start is called before the first frame update
    private void Awake()
    {
        instantiatedObjects = new GameObject[80];
    }
    void Start()
    {
        InstantiateAllObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (!StateManager.isPaused)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                SpawnObstacles();
                timer = 0;
            }
            MoveObstacles();
        }
       
    }

    public void SpawnObstacles()
    {
        var instance = instantiatedObjects[Random.Range(0, 80)];               
        if (!instance.activeSelf)
        {
            instance.transform.position = start.transform.position;
            instance.SetActive(true);
        }
        else
        {
            SpawnObstacles();
        }
    }
 
    public void MoveObstacles()
    {
        for (int i = 0; i < instantiatedObjects.Length; i++)
        {
            var thevector = Vector3.left * Time.deltaTime * speed;
            instantiatedObjects[i].transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self);
            if (instantiatedObjects[i].transform.position.x <= goal.transform.position.x)
            {
                instantiatedObjects[i].SetActive(false);
                instantiatedObjects[i].transform.position = start.transform.position;                           
            }
        }
    }
    public void InstantiateAllObjects()
    {       
            for (int x = 0; x < 80; x++)
            {
                var instance = Instantiate(easyObstacles[Random.Range(0,8)], start.transform.position + new Vector3(-10, 0, 0), Quaternion.identity);
                instantiatedObjects[x] = instance;
                instance.SetActive(false);
            }                 
    }
}
