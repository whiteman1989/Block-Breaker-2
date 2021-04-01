using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    AudioClip destroySound;

    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        level.CountBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BlockDestring();
    }

    private void BlockDestring()
    {
        AudioSource.PlayClipAtPoint(destroySound, transform.position);
        Destroy(gameObject, 0.01f);
        level.BlockDestroyed();
    }
}
