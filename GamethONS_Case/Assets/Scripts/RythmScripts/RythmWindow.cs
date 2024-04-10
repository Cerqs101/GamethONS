using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class RythmWindow : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private SpriteRenderer spriteRenderer;
    public static RythmWindow Instance;
    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = virtualCamera.transform.position + new Vector3(0f, 2f, 1f);

        if(LevelManager.isEncounterHappening)
            spriteRenderer.enabled = true;
        else
            spriteRenderer.enabled = false;
    }
}
