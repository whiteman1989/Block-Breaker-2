using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    [Header("Gameplay setings")]
    [SerializeField]
    private Paddle parentPaddle;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float randomizeLunchAngle = 5f;
    [SerializeField]
    private int maxBounveRepeat = 3;
    [SerializeField]
    [Range(0f, 180f)]
    private float repeatAngleDetection = 1f;
    [Header("FxSetings")]
    [SerializeField]
    AudioClip[] hitSounds;
    [SerializeField]
    [Range(0, 45)]
    private float randomizeLoppAngle = 2f;
    [Header("States")]
    [ReadOnly, SerializeField]
    private int curentLoppBounces = 0;

    private Rigidbody2D myRigidbody;
    private AudioSource audioSource;
    private Level level;
    private Vector2 toPaddleVector;
    private bool isLocked = true;
    private Vector2 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        LockBall();
        audioSource = GetComponent<AudioSource>();
        level = Object.FindObjectOfType<Level>();
        level.AddBall(this);
        lastVelocity = myRigidbody.velocity;
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
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawLine(contact.point, contact.point + contact.relativeVelocity/5, Color.green, 2, false);
            Debug.DrawLine(contact.point, contact.point + Vector2.Reflect((contact.relativeVelocity*-1),
                           contact.normal)/5,
                           Color.red, 2, false);
        }
        audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SpeedControlProcess();
        RepeatHandle();
    }

    private void OnDestroy()
    {
        level.RemoveBall(this);
    }


    public void LunchBall()
    {
        float randomAngle = Random.Range(-randomizeLunchAngle, randomizeLunchAngle);
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
        toPaddleVector = transform.position - parentPaddle.transform.position;
    }

    private void LockBallProcess()
    {
        transform.position = (Vector2)parentPaddle.transform.position + toPaddleVector;
    }

    private void SpeedControlProcess()
    {
        myRigidbody.velocity = myRigidbody.velocity.normalized * speed;
    }

    private void RepeatHandle()
    {
        float angle = Vector2.Angle(myRigidbody.velocity, lastVelocity * -1);
        if (angle <= repeatAngleDetection)
        {
            curentLoppBounces++;
            if(curentLoppBounces > maxBounveRepeat) PreventLoop();
        }
        else
        {
            if(angle <= 180f-repeatAngleDetection)curentLoppBounces = 0;
        }
        lastVelocity = myRigidbody.velocity;
    }

    private void PreventLoop()
    {
        float randomAngle = Random.Range(-randomizeLoppAngle, randomizeLoppAngle);
        myRigidbody.velocity = (myRigidbody.velocity.GetRotatefdVector2(randomAngle));
        Debug.Log($"Loop #${curentLoppBounces} prevented, correction angele is {randomAngle}");
    }

    public void SetParrent(Paddle paddle)
    {
        parentPaddle = paddle;
    }
}
