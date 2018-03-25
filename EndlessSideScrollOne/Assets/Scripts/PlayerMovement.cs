using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float jumpSize = 5.0f;
    Rigidbody2D iRigidBody;
    bool isGrounded = false;
    float positionX = 0.0f;
    bool gameOver = false;
    Scroller myScroller;
    GameControl myGameControl;
    public AudioClip gameEnd;
    public AudioClip backGround;
    public AudioClip collide;
    public AudioClip jump;
    public AudioClip scoreSound;
    AudioSource myAudioSource;

    // Use this for initialization
	void Start () {
        iRigidBody = transform.GetComponent<Rigidbody2D>();
        positionX = transform.position.x;
        myScroller = GameObject.FindObjectOfType<Scroller>();
        myGameControl = GameObject.FindObjectOfType<GameControl>();
        myAudioSource = GameObject.FindObjectOfType<AudioSource>();
        myAudioSource.PlayOneShot(backGround);

    }
	
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.Space) && isGrounded && !gameOver)
        {
            iRigidBody.AddForce(Vector3.up * (jumpSize * iRigidBody.mass * iRigidBody.gravityScale * 20.0f));
            myAudioSource.PlayOneShot(jump);
        }

        //statement to check if Player has collided with an Obstacle.
        //calls the GameOver function.
        if (iRigidBody.position.x < positionX) //calculates position of Player vs Obstacle
        {
            myAudioSource.PlayOneShot(collide);
            GameOver(); //Calls Game Over function
        }
		
	}
    void Update()
    {
        
    }

    //The Game Over function- called when player gets 'hit' by obstacles
    void GameOver()
    {
        gameOver = true;
        myAudioSource.PlayOneShot(gameEnd);
        myScroller.GameOver();
    }

    //Functions to check whether the player is on the ground or not
    //Player CANNOT jump unless on the Ground.
    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = true; //Changes status of Player to true (Meaning player is on ground surface) allowing them to jump
        }
        if (other.collider.tag == "Enemy")
        {
            GameOver();
        }
    }

    void OnCollisionStay2D (Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = true; //Changes status of Player to true, allowing them to jump
        }
    }
    void OnCollisionExit2D (Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = false; //Player is in mid-jump, CANNOT jump again.
        }
    }

    
    void OnTriggerEnter2D (Collider2D other) //For score keeping. See GameControl class
    {
        if (other.tag == "Coin")
        {
            myAudioSource.PlayOneShot(scoreSound);
            myGameControl.ScoreCalc(); //calls function ScoreCalc() from GameControl class.
            Destroy(other.gameObject); //destroys the 'coin' upon player collision.
            
        }
    }
}
