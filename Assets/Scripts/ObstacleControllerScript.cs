using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControllerScript : MonoBehaviour
{
    public GameManagerScript gameManager;
    public GameObject[] set_obstacles;
    public GameObject[] combo_obstacles;
    public float interval_S = 1.25f;
    public float time_S = 1.1f;
    public float interval_C = 1.5f;
    public float time_C = .75f;
    public float countdown = 0f;
    public float moveSpeed = 9.5f;
    public float deathzone = -30f;
    private bool isGameOver = false;
    private int obstacleChoice;
    private float interval;
    private float time;

    void FixedUpdate()
    {
        if (isGameOver) return;
        Spawn_Obstacles();
        Manage_Obstacles();
    }

    private void Spawn_Obstacles()
    {
        if (countdown <= 0)
        {
            obstacleChoice = Random.Range(0, 2);
            if(obstacleChoice == 0)
            {
                interval = interval_S;
                time = time_S;
                Spawn(set_obstacles);
            } else if (obstacleChoice == 1)
            {
                interval = interval_C;
                time = time_C;
                Spawn(combo_obstacles);
            }
        } else 
        {
            countdown -= Time.deltaTime * time;
        }
    }

    private void Manage_Obstacles()
    {
        GameObject currChild;
        for(int i = 0; i < transform.childCount; i++)
        {
            currChild = transform.GetChild(i).gameObject;
            if (currChild.transform.position.x <= deathzone)
            {
                Destroy(currChild);
            }
            currChild.transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        }
    }

    private void Spawn(GameObject[] obstacles)
    {
            GameObject newObstacle = Instantiate(obstacles[Random.Range(0,obstacles.Length)], transform.position, Quaternion.identity);
            newObstacle.transform.parent = transform;
            countdown = interval;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameManager.SendMessage("GameOver");
    }
}