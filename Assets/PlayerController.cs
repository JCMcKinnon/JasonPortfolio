using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    private Rigidbody2D rb;
    public GameObject child;
    private SpriteShapeRenderer ren;
    private Camera cam;
    public bool playerDead;

    private bool tweening;
    void Start()
    {
       
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ren = GetComponent<SpriteShapeRenderer>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var scaleTo = new Vector3(0.05f, -0.05f, 0);
            rb.AddForce(Vector2.up * 15f,ForceMode2D.Impulse);
           // cam.DOShakePosition(0.5f,0.1f);

            if (!tweening)
            {
                tweening = true;
                transform.DOPunchScale(scaleTo, 0.2f, 1, 0.5f).OnComplete(FinishTween);
            }
            if (StateManager.isPaused)
            {

                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
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
        cam.DOShakePosition(0.2f, 1f,4,10);

        playerDead = true;
        ren.enabled = false;
        child.SetActive(true);
        StateManager.isPaused = true;
        
    }

    public void FinishTween()
    {
        tweening = false;
    } 
}
