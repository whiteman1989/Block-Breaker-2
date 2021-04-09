using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float maxX = 8f;
    [SerializeField]
    private float minX = -8f;
    [SerializeField]
    private float paddleSpeed = 0.5f;
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private Vector2 spawnOffset;

    private GameSession gameSession;
    private Level level;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        spawnBall();
        level = FindObjectOfType<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        MovingPaddle();
        InputHandler();
    }

    public void spawnBall()
    {
        Vector3 offset = spawnOffset;
        var newball = Instantiate(ballPrefab, transform.position + offset, transform.rotation);
        newball.GetComponent<Ball>().SetParrent(this);
    }

    private void InputHandler()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spawnBall();
        }
    }

    private void MovingPaddle()
    {
        Vector2 targetPos = new Vector2(GetPosition(), transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, paddleSpeed * Time.deltaTime);
    }

    private float GetPosition()
    {
        if(!gameSession.IsAutoPlay)
        {
            var worldMousePositionX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            return Mathf.Clamp(worldMousePositionX, minX, maxX);
        }
        else
        {
            Vector2 closestBall = new Vector2(0f,0f);
            float closestDistance = Mathf.Infinity;
            foreach(var ball in level.Balls)
            {
                var fallingTime = (ball.transform.position.y+6) / (ball.GetComponent<Rigidbody2D>().velocity.y*-1);
                if (fallingTime < closestDistance && fallingTime > 0)
                {
                    closestBall = ball.transform.position;
                    closestDistance = fallingTime;
                }
            }
            return Mathf.Clamp(closestBall.x, minX, maxX);

        }
    }
}
