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
    private GameObject ballPrefab;
    [SerializeField]
    private Vector2 spawnOffset;

    // Start is called before the first frame update
    void Start()
    {
        spawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        var worldMousePositionX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        Vector2 paddlePos = new Vector2(Mathf.Clamp(worldMousePositionX, minX, maxX), transform.position.y);
        transform.position = paddlePos;
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
}
