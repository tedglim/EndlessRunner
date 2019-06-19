using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class P1Script : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float jumpForce = 750f;
    public float fallMultiplier = 2.5f;
    private bool isGroundedL;
    private bool isGroundedR;
    public Transform stageCheckL;
    public Transform stageCheckR;
    public float checkRadiusL;
    public float checkRadiusR;
    public LayerMask whatIsStage;

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
            CheckGrounded();
        }
        if (transform.position.x < posX)
        {
            GameOver();
        }
    }

    void CheckGrounded()
    {
        isGroundedL = Physics2D.OverlapCircle(stageCheckL.position, checkRadiusL, whatIsStage);
        isGroundedR = Physics2D.OverlapCircle(stageCheckR.position, checkRadiusR, whatIsStage);
    }

    void FixedUpdate()
    {
        if(isGameOver)
        {
            return;
        }
        if(wantsJump && (isGroundedL || isGroundedR) && !isGameOver)
        {
            rb2d.AddForce(Vector2.up * jumpForce);
        }
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        wantsJump = false;
    }

    void GameOver()
    {
        isGameOver = true;
        obstacleController.GameOver();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Coin")
        {
            gameManager.AddScore();
            Destroy(collider.gameObject);
        } else if (collider.tag == "Spike")
        {
            CameraShaker.Instance.ShakeOnce(2f, 3f, .1f, .5f);
            GameOver();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(stageCheckL.position, checkRadiusL);
        Gizmos.DrawWireSphere(stageCheckR.position, checkRadiusR);
    }
}