using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Magnet : MonoBehaviour
{
    [Header("Gameplay options")]
    [SerializeField]
    private bool isEnable = false;
    [SerializeField]
    private int timeDelay = 0;

    [Header("FX options")]
    [SerializeField]
    private Sprite enableSprite;
    private Sprite disableSprite;

    private PointEffector2D pointEffector;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        pointEffector = GetComponent<PointEffector2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        disableSprite = spriteRenderer.sprite;
        particleSystem = GetComponentInChildren<ParticleSystem>();
        if(isEnable)
        {
            EnableMagnet();
        }
        else
        {
            DisableMagnet();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timeDelay > 0 && !isEnable)
        {
            WaitAndRun();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DisableMagnet();
    }

    private async void AsingTest()
    {
        await Task.Delay(3000);
        Debug.LogWarning("Task ready");
    }

    private async void WaitAndRun()
    {
        isEnable = true;
        await Task.Delay(timeDelay);
        EnableMagnet();

    }

    public void EnableMagnet()
    {
        spriteRenderer.sprite = enableSprite;
        pointEffector.enabled = true;
        particleSystem.Play();
    }

    public void DisableMagnet()
    {
        spriteRenderer.sprite = disableSprite;
        pointEffector.enabled = false;
        particleSystem.Stop();
        isEnable = false;
    }
}
