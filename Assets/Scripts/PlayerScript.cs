using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour {

    public float jumpPower = 10.0f;
    Rigidbody2D myRigidbody;
    bool isGrounded = false;
    float posX = 0.0f;
    bool isGameOver = false;
    ChallengeController myChallengeController;
    GameController myGameController;
    public AudioClip jump;
    AudioSource myAudioPlayer;
    public AudioClip scoreSFX;
    public AudioClip deadSFX;

    // Use this for initialization
    void Start () {
        myRigidbody = transform.GetComponent<Rigidbody2D>();
        posX = transform.position.x;
        myChallengeController = GameObject.FindObjectOfType<ChallengeController>();
        myGameController = GameObject.FindObjectOfType<GameController>();
        myAudioPlayer = GameObject.FindObjectOfType<AudioSource>();
    }
	
	
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.Space) && isGrounded && !isGameOver) {
            myRigidbody.AddForce(Vector3.up * (jumpPower * myRigidbody.mass * myRigidbody.gravityScale * 20.0f));
            myAudioPlayer.PlayOneShot(jump);
            isGrounded = false;
        }
        //Hit in face check
        if ((Math.Abs(transform.position.x - posX) > 0.2) && !isGameOver) {
            GameOver();
        }


	}

    void GameOver() {
        isGameOver = true;
        myAudioPlayer.PlayOneShot(deadSFX);
        myChallengeController.GameOver();
    }



    void OnCollisionStay2D(Collision2D other)
    {

        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
        }

    }


    void OnCollisionExit2D(Collision2D other)
    {

        if (other.collider.tag == "Ground")
        {
            isGrounded = false;
        }

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Star") {
            myGameController.IncrementScore();
            myAudioPlayer.PlayOneShot(scoreSFX);
            Destroy(other.gameObject);
        }
    }
}
