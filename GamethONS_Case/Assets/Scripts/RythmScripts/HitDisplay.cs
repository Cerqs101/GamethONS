using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDisplay : MonoBehaviour
{
    private Coroutine currentCoroutine;
    private bool isCoroutineRunning = false;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void SetTemporarySprite(Sprite sprite, float waitTime=1f)
    {
        ChangeSprite(sprite);
        isCoroutineRunning = true;
        currentCoroutine = StartCoroutine(FadeOut(waitTime));
        isCoroutineRunning = false;
    }


    public void ChangeSprite(Sprite sprite)
    {
        if(isCoroutineRunning)
        {
            isCoroutineRunning = false;
            StopCoroutine(currentCoroutine);
        }
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        spriteRenderer.sprite = sprite;
    }


    private IEnumerator FadeOut(float waitTime=0.5f)
    {
        float waitPerLoop = waitTime/255f;
        Color32 lessAlpha = new Color32(0,0,0,1);
        while(spriteRenderer.color != new Color32(255,255,255,0))
        {
            spriteRenderer.color -= lessAlpha;
            yield return new WaitForSecondsRealtime(waitPerLoop);
        }
    }
}
