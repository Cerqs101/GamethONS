using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class RythmWindow : MonoBehaviour
{
    private Camera virtualCamera;
    private SpriteRenderer spriteRenderer;
    public static RythmWindow Instance;


    void Start()
    {
        virtualCamera = FindObjectOfType<Camera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        transform.position = virtualCamera.transform.position + new Vector3(0f, 2f, 1f);

        if (LevelManager.isEncounterHappening)
        {
            if(spriteRenderer.enabled != true)
                changeChildrenSpriteRendererEnable(true);
                spriteRenderer.enabled = true;
        }
        else
        {
            if(spriteRenderer.enabled != false)
                changeChildrenSpriteRendererEnable(false);
                spriteRenderer.enabled = false;
        }
    }


    void changeChildrenSpriteRendererEnable(bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            SpriteRenderer childSpriteRenderer = transform.GetChild(i).GetComponent<SpriteRenderer>();
            if (childSpriteRenderer != null)
                childSpriteRenderer.enabled = state;
        }
    }
}
