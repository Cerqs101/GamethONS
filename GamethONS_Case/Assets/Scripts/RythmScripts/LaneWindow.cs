using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class LaneWindow : MonoBehaviour
{
    private Camera virtualCamera;
    private SpriteRenderer spriteRenderer;
    private Vector3 distanceFromCenter;


    void Start()
    {
        virtualCamera = FindObjectOfType<Camera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        distanceFromCenter = new Vector3(0f, 2f + transform.position.y - transform.parent.position.y, 1f);
    }


    void Update()
    {
        transform.position = virtualCamera.transform.position + distanceFromCenter;

        if (LevelManager.isEncounterHappening)
        {
            if(spriteRenderer.enabled != true)
            {
                changeChildrenSpriteRendererEnableState(true, transform);
                spriteRenderer.enabled = true;
            }
        }
        else
        {
            if(spriteRenderer.enabled != false)
            {
                changeChildrenSpriteRendererEnableState(false, transform);
                spriteRenderer.enabled = false;
            }
        }
    }


    void changeChildrenSpriteRendererEnableState(bool state, Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            SpriteRenderer childSpriteRenderer = transform.GetChild(i).GetComponent<SpriteRenderer>();
            if (childSpriteRenderer != null)
                childSpriteRenderer.enabled = state;
            if(transform.GetChild(i).childCount != 0)
                changeChildrenSpriteRendererEnableState(state, child);
        }
    }
}
