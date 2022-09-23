using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float MovementSpeed;
    public float MovementIncrement;
    Vector2 BallDirection = new Vector2(1, 1);

    // Only runs once at the start before the first frame
    void Start()
    {
        ResetBall();
        RandomizeBallDirection();
    }

    // Update function that updates every frame
    void Update()
    {
        // Moves the ball
        transform.Translate(BallDirection * MovementSpeed * Time.deltaTime);
        MovementSpeed += MovementIncrement * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collission)
    {
        if (collission.gameObject.CompareTag("Wall"))
        {
            BallDirection = Vector2.Reflect(BallDirection, collission.contacts[0].normal);
        }
        if (collission.gameObject.CompareTag("Paddle"))
        {
            BallDirection = Vector2.Reflect(BallDirection, collission.contacts[0].normal);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftBarrier"))
        {
            ResetBall();
            GameObject.Find("ScoreCanvas").GetComponent<ScoreController>().AddP2Score();
        }
        if (collision.gameObject.CompareTag("RightBarrier"))
        {
            ResetBall();
            GameObject.Find("ScoreCanvas").GetComponent<ScoreController>().AddP1Score();
        }
    }

    private void ResetBall()
    {
        MovementSpeed = 5f;
        transform.position = new Vector2(0, 0);
        
        RandomizeBallDirection();
    }

    private void RandomizeBallDirection()
    {
        BallDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        if (BallDirection.x > -0.1f && BallDirection.x < 0.1f)
        {
            BallDirection = new Vector2(0.5f, BallDirection.y);
        }

        BallDirection = BallDirection.normalized;

    }
}
