using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Gameplay options")]
    [SerializeField, Range(0.0f, 5f)]
    private float gameSpeed = 1f;
    [SerializeField]
    private bool isAupoPlay = false;


    [ReadOnly]
    [SerializeField]
    private int currentScore = 0;

    public int CurentScore { get => currentScore; }
    public bool IsAutoPlay { get => isAupoPlay; }

    private void Awake()
    {
        if(FindObjectsOfType<GameSession>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        ScoreTextInit();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ScoreTextInit();
    }

    private void ScoreTextInit()
    {
        scoreText = GameObject.Find("Score Text")?.GetComponent<TextMeshProUGUI>();
        if (scoreText is object) scoreText.text = currentScore.ToString();
    }
}
