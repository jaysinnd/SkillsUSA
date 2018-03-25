using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{

    public float scrollSpeed = 5.0f;
    public GameObject[] obstacles;
    public float frequency = 0.5f;
    float counter = 0.0f;
    public Transform obstaclesSpawn;
    bool gameOver = false;


    // Use this for initialization
    void Start()
    {
        RandomGenerateObstacle();
    }

    // Update is called once per frame
    void Update() //Generates our obstacles off screen to the right
    {
        if (gameOver) return;

        if (counter <= 0.0f)
        {
            RandomGenerateObstacle(); //calls the Random Generate Obstacle function
        }
        else
        {
            counter -= Time.deltaTime * frequency;
        }


        GameObject currentChild;
        for (int i = 0; i < transform.childCount; i++)
        {
            currentChild = transform.GetChild(i).gameObject;
            Scroll(currentChild);

            if (currentChild.transform.position.x <= -15.0f) //Destroys obstacle objects once they are so far off screen
            {
                Destroy(currentChild);
            }
        }
    }
    void Scroll(GameObject currentChallenge)
    {
        currentChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
    }
    void RandomGenerateObstacle() //Generates all our obstacles to challenge the player.
    {
        GameObject newObstacles = Instantiate(obstacles[Random.Range(0, obstacles.Length)], obstaclesSpawn.position, Quaternion.identity) as GameObject;
        newObstacles.transform.parent = transform;
        counter = 1.0f;
    }

    public void GameOver()
    {
        gameOver = true;
        transform.GetComponent<GameControl>().GameOver();
    }

    
}
