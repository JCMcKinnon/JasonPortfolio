using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Queue<GameObject> backgroundObjects;
    public Queue<GameObject> backgroundObjects2;

    public GameObject backgroundObject;
    public GameObject backgroundObject2;
    private float timer;

    public GameObject startObject;

    public GameObject[] backgroundObjectsArray1;
    public GameObject[] backgroundObjectsArray2;
    public GameObject goal;

    public int ammountToSpawn;

    public GameObject Canvas;

    public void Awake()
    {
        backgroundObjects = new Queue<GameObject>();
        backgroundObjects2 = new Queue<GameObject>();
        backgroundObjectsArray1 = new GameObject[ammountToSpawn];
        backgroundObjectsArray2 = new GameObject[ammountToSpawn];


    }
    public void Start()
    {
        //spawn background
        for (int i = 0; i < ammountToSpawn; i++)
        {
            var instance = Instantiate(backgroundObject, new Vector3(0, 0, 0), Quaternion.identity * backgroundObject.transform.rotation);
            instance.transform.position = startObject.transform.position + new Vector3(i * -2,startObject.transform.position.y, startObject.transform.position.z);
            backgroundObjects.Enqueue(instance);
            backgroundObjectsArray1[i] = instance;

            //instance.SetActive(false);
        }
        for (int y = 0; y < ammountToSpawn; y++)
        {
            var instance = Instantiate(backgroundObject2, new Vector3(0, 0, 0), backgroundObject2.transform.rotation);
            instance.transform.position = startObject.transform.position  + new Vector3(y * -2, startObject.transform.position.y, startObject.transform.position.z);
            backgroundObjects2.Enqueue(instance);
            backgroundObjectsArray2[y] = instance;

            //instance.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!StateManager.isPaused)
        {
            timer += Time.deltaTime;
            // PlaceBackgroundAtStartPosition();
            for (int i = 0; i < backgroundObjectsArray1.Length; i++)
            {
                ConstantlyMoveBackgroundObject(backgroundObjectsArray1[i]);
                ConstantlyMoveBackgroundObject(backgroundObjectsArray2[i]);
                CheckIfAtGoalAndMoveToStart(backgroundObjectsArray1[i], goal.transform.position);
                CheckIfAtGoalAndMoveToStart(backgroundObjectsArray2[i], goal.transform.position);
            }
        }


        if (StateManager.isPaused)
        {
            Canvas.SetActive(true);

        }
        else
        {
            Canvas.SetActive(false);
        }
  
    }

    public void PlaceBackgroundAtStartPosition()
    {
        if(timer > 0.5f)
        {
            var instance = backgroundObjects.Dequeue();
            instance.SetActive(true);
            timer = 0;
        }
    }

    public void ConstantlyMoveBackgroundObject(GameObject objectToMove)
    {
        objectToMove.transform.position += Vector3.left * 5 * Time.deltaTime;
    }
    public void CheckIfAtGoalAndMoveToStart(GameObject obj, Vector3 goalPos)
    {
        if(obj.transform.position.x <= goalPos.x)
        {
            obj.transform.position = startObject.transform.position;
        }
    }
}
