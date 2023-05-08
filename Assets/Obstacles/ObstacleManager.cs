using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] easyObstacles;
    public GameObject[] hardObstacles;

    public GameObject[] instantiatedObjects;
    public float spawnWeight;

    private float timer;
    public GameObject start;
    private GameObject goal;

    public float speed;
    // Start is called before the first frame update
    private void Awake()
    {
        instantiatedObjects = new GameObject[4];
    }
    void Start()
    {
        // Instantiate(easyObstacles[0], Vector3.zero, Quaternion.identity);
        InstantiateAllObjects();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 
        if(timer > 0.7f)
        {
            SpawnObstacles();
            timer = 0;
        }
        MoveObstacles();
    }

    public void SpawnObstacles()
    {
        var instance = instantiatedObjects[Random.Range(-1, 5)];

        if (!instance.activeSelf)
        {
            instance.transform.position = start.transform.position;
            instance.SetActive(true);
        }
        else
        {
            instance = instantiatedObjects[Random.Range(-1, 5)];
        }
       
    }

    public void MoveObstacles()
    {
        for (int i = 0; i < 4; i++)
        {
            if (instantiatedObjects[i].activeSelf)
            {
                instantiatedObjects[i].transform.position += Vector3.left * Time.deltaTime * speed;
            }
        }
    }
    public void InstantiateAllObjects()
    {
        for (int i = 0; i < 4; i++)
        {
            var easyInstance = Instantiate(easyObstacles[i], start.transform.position + new Vector3(-10,0,0), Quaternion.identity);
            var hardInstance = Instantiate(hardObstacles[i], start.transform.position, Quaternion.identity);
            instantiatedObjects[i] = (easyInstance);
            easyInstance.SetActive(false);
            hardInstance.SetActive(false);
        }
    }
}
