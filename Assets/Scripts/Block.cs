using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("FX config")]
    [SerializeField]
    private AudioClip destroySound;
    [SerializeField]
    private GameObject blockSparklesVFX;
    [Header("GamePlay options")]
    [SerializeField]
    private int points = 50;
    [SerializeField]
    private bool isBreakable = true;
    [SerializeField]
    private Sprite[] hitSprites;

    [Header("State variables")]
    [ReadOnly]
    [SerializeField]
    private int hitsCount = 0;

    private Level level;
    private GameSession gameStatus;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameSession>();
        level = FindObjectOfType<Level>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(isBreakable) level.CountBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBreakable)
        {
            hitsCount++;
            if (hitsCount > hitSprites.Length)
            { 
                BlockDestroing(); 
            }
            else
            {
                ShowNextSprite();
            }
        }
    }

    private void ShowNextSprite()
    {
        spriteRenderer.sprite = hitSprites[hitsCount-1];
    }

    private void BlockDestroing()
    {
        AudioSource.PlayClipAtPoint(destroySound, transform.position);
        gameStatus.AddToScore(points);
        TriggerSparklesVFX();
        Destroy(gameObject, 0.01f);
        level.BlockDestroyed();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparcles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparcles, 2f);
    }
}
