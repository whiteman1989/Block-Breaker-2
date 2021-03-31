using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private Paddle firstPaddle;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float randomizationLunchAngle = 5f;
    [SerializeField]
    AudioClip[] hitSounds;

    private Rigidbody2D myRigidbody;
    private AudioSource audioSource;
    private Vector2 toPaddleVector;
    private bool isLocked = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        LockBall();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked) 
        { 
            LockBallProcess();
            if (Input.GetMouseButtonDown(0))
            {
                UnlockBall();
                LunchBall();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Vector is {myRigidbody.velocity.GetVectorAngle()}" +
                $" and speed is {myRigidbody.velocity.magnitude}");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SpeedControlProcess();
    }


    public void LunchBall()
    {
        float randomAngle = Random.Range(-randomizationLunchAngle, randomizationLunchAngle);
        Debug.Log($"Lunch angle is {randomAngle}");
        myRigidbody.velocity = (Vector2.up.GetRotatefdVector2(randomAngle))*speed;
        
    }

    public void LockBall()
    {
        GetLockRelationPosition();
        isLocked = true;
    }

    public void UnlockBall()
    {
        isLocked = false;
    }

    private void GetLockRelationPosition()
    {
        toPaddleVector = transform.position - firstPaddle.transform.position;
    }

    private void LockBallProcess()
    {
        transform.position = (Vector2)firstPaddle.transform.position + toPaddleVector;
    }

    private void SpeedControlProcess()
    {
        myRigidbody.velocity = myRigidbody.velocity.normalized * speed;
        Debug.Log("Speed corected");
    }
}
