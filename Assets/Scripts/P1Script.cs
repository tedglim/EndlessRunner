using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Script : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float jumpForce = 750f;
    public float fallMultiplier = 2.5f;
    private bool isGrounded;
    private bool wantsJump;
    private bool isGameOver = false;
    private float posX;
    public ObstacleControllerScript obstacleController;
    public GameManagerScript gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        posX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            wantsJump = true;
        }
        if (transform.position.x < posX)
        {
            GameOver();
        }
    }

    void FixedUpdate()
    {
        if(wantsJump && isGrounded == true && !isGameOver)
        {
            rb2d.AddForce(Vector2.up * jumpForce);
        }
        wantsJump = false;
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
    }

    void GameOver()
    {
        isGameOver = true;
        obstacleController.GameOver();
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D (Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Coin")
        {
            gameManager.AddScore();
            Destroy(collider.gameObject);
        } else if (collider.tag == "Spike")
        {
            GameOver();
        }
    }
}