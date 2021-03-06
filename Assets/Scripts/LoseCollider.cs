using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    [SerializeField]
    private bool isGameOverTriger = false;
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = GameObject.Find("Scene Loader").GetComponent<SceneLoader>();
        if (sceneLoader is object) { Debug.Log("Scene loader finded"); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Destroy(collision.gameObject, 1f);
            if(isGameOverTriger)
            {
                sceneLoader?.LoadGameOver();
            }
        }
    }
}
