using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    private Rigidbody2D rb;
    public GameObject child;
    private SpriteShapeRenderer ren;

    public bool playerDead;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ren = GetComponent<SpriteShapeRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * 15f,ForceMode2D.Impulse);
            if (StateManager.isPaused)
            {
                transform.position = new Vector3(-5.3f, 0, 0);
                ren.enabled = true;
                child.SetActive(false);
                StateManager.isPaused = false;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.AddForce(-Vector2.up * 8f, ForceMode2D.Impulse);


        }



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerDead = true;
        ren.enabled = false;
        child.SetActive(true);
        StateManager.isPaused = true;
        
    }
}
