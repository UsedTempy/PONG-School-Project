using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float MovementSpeed;
    public float MovementIncrement;

    public Text CountDown;
    private bool isCountingDown = true;

    [SerializeField] ParticleSystem ExplosionParticle = null;

    Vector2 BallDirection = new Vector2(1, 1);

    // Only runs once at the start before the first frame
    void Start()
    {
        StartCoroutine(StartCountdown());     
    }

    // Update function that updates every frame
    void Update()
    {
        // Moves the ball
        if (isCountingDown == false)
        {
            transform.Translate(BallDirection * MovementSpeed * Time.deltaTime);
            MovementSpeed += MovementIncrement * Time.deltaTime;
        } else
        {
            transform.position = new Vector2(0, 0);
        }
        
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
        StartCoroutine(HandleCoolScoreEffect(3));
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

    public IEnumerator HandleCoolScoreEffect(float n)
    {
        MovementIncrement = 0;
        MovementSpeed = 0;

        ExplosionParticle.Play();

        gameObject.GetComponent<TrailRenderer>().enabled = false;

        yield return new WaitForSeconds(n);

        MovementSpeed = 5f;
        transform.position = new Vector2(0, 0);

        gameObject.GetComponent<TrailRenderer>().enabled = true;

        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown()
    {
        CountDown.GetComponent<Text>().enabled = true;
        transform.position = new Vector2(0, 0);
        isCountingDown = true;

        CountDown.text = "3";
        yield return new WaitForSeconds(1);
        CountDown.text = "2";
        yield return new WaitForSeconds(1);
        CountDown.text = "1";
        yield return new WaitForSeconds(1);
        CountDown.text = "GO!";
        yield return new WaitForSeconds(1);

        isCountingDown = false;
        CountDown.GetComponent<Text>().enabled = false;

        RandomizeBallDirection();
    }
}
