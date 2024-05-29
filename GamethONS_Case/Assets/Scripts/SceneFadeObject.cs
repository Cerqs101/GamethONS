using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeObject : MonoBehaviour
{
    
    private Image image;
    [SerializeField] public float waitTime=0.5f;
    [SerializeField] private bool fadeOnStart = true;
    private float waitPerLoop;
    private Color32 opacityReduction;

    private Coroutine currentFade;
    private bool isFading = false;

    public static SceneFadeObject instance;


    void Start()
    {
        image = GetComponent<Image>();
        waitPerLoop = waitTime/(256f*2);
        opacityReduction = new Color32(0,0,0,1);
        instance = this;

        if(fadeOnStart)
        {
            image.color = new Color32(0,0,0,255);
            Fade("out");
        }
        else    
            image.color = new Color32(0,0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void Fade(string mode)
    {
        if(isFading && currentFade != null)
            StopCoroutine(currentFade);

        if(mode == "in")
            currentFade = StartCoroutine(FadeIn());
        else if(mode == "out")
            currentFade = StartCoroutine(FadeOut());
        else
            return;
    
        isFading = true;
    }


    public IEnumerator FadeOut()
    {
        isFading = true;
        while(image.color.a > 0)
        {
            image.color -= opacityReduction;
            yield return new WaitForSecondsRealtime(waitPerLoop);
        }
        isFading = false;
    }


    public IEnumerator FadeIn()
    {
        isFading = true;
        while(image.color.a < 255)
        {
            image.color += opacityReduction;
            yield return new WaitForSecondsRealtime(waitPerLoop);
        }
        isFading = false;
    }


    // public IEnumerator Fade(string type)
    // {
    //     while(spriteRenderer.color != new Color32(255,255,255,255))
    //     {
    //         spriteRenderer.color += opacityReduction;
    //         yield return new WaitForSecondsRealtime(waitPerLoop);
    // }
}
