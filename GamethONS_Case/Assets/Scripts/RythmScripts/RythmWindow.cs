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
                changeChildrenSpriteRendererEnable(true, transform);
                spriteRenderer.enabled = true;
        }
        else
        {
            if(spriteRenderer.enabled != false)
                changeChildrenSpriteRendererEnable(false, transform);
                spriteRenderer.enabled = false;
        }
    }


    void changeChildrenSpriteRendererEnable(bool state, Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            SpriteRenderer childSpriteRenderer = transform.GetChild(i).GetComponent<SpriteRenderer>();
            if (childSpriteRenderer != null)
                childSpriteRenderer.enabled = state;
            if(transform.GetChild(i).childCount != 0)
                changeChildrenSpriteRendererEnable(state, child);
        }
    }
}
