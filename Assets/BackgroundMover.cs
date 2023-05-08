using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public float speed;
    public Transform objectTransform;
    public bool reachedGoal;

    public GameObject goal;
    public GameObject start;



    public GameManager gm;

    // Start is called before the first frame update
    private void Awake()
    {
        reachedGoal = false;

        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        objectTransform = transform;  
    }
    
    public void OnDisable()
    {
        reachedGoal = false;
    }
    public void CheckIfReachedGoal()
    {
        if(transform.position.x <= goal.transform.position.x)
        {
            print(reachedGoal);
            reachedGoal = true;

        }
    }
    // Update is called once per frame
    public void Update()
    {
        objectTransform.position += Vector3.left * speed * Time.deltaTime;
        CheckIfReachedGoal();
        if (reachedGoal)
        {
            print(reachedGoal);
            transform.position = start.transform.position;
            gameObject.SetActive(false);
            gm.backgroundObjects.Enqueue(gameObject);
        }
    }
}
