using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [ReadOnly]
    [SerializeField]
    private int breakableBlocks = 0;
    [ReadOnly]
    [SerializeField]
    private int ballsCount = 0;

    private SceneLoader sceneLoader;
    private HashSet<Ball> balls = new HashSet<Ball>();

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if(breakableBlocks <=0) { sceneLoader.LoadNextScene(); }
    }

    public int AddBall(Ball ball)
    {
        balls.Add(ball);
        ballsCount = balls.Count;
        return ballsCount;
    }

    public int RemoveBall(Ball ball)
    {
        balls.Remove(ball);
        ballsCount = balls.Count;
        if (ballsCount <= 0) sceneLoader?.LoadGameOver();
        return ballsCount;
    }
}
