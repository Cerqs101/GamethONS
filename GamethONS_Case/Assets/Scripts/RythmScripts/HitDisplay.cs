using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDisplay : MonoBehaviour
{
    private Coroutine currentFadeOut;
    private bool isFadingOut = false;

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void SetTemporarySprite(Sprite sprite, float waitTime=1f)
    {
        ChangeSprite(sprite);
        isFadingOut = true;
        currentFadeOut = StartCoroutine(FadeOut(waitTime));
    }


    public void ChangeSprite(Sprite sprite)
    {
        if(isFadingOut)
        {
            StopCoroutine(currentFadeOut);
            isFadingOut = false;
        }
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        spriteRenderer.sprite = sprite;
    }


    private IEnumerator FadeOut(float waitTime=0.5f)
    {
        float waitPerLoop = waitTime/255f;
        Color32 opacityReduction = new Color32(0,0,0,1);
        while(spriteRenderer.color != new Color32(255,255,255,0))
        {
            spriteRenderer.color -= opacityReduction;
            yield return new WaitForSecondsRealtime(waitPerLoop);
        }
        isFadingOut = false;
    }
}
