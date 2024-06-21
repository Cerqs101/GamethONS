using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterfallScroll : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 1f;
    private float camHeight;
    private float objectHeight;
    private int amountOfBrothers;

    private Camera cam;
    private SpriteRenderer spriteRenderer;
    private TilemapRenderer tilemapRenderer;


    // Start is called before the first frame update
    void Awake()
    {
        if(GetComponent<SpriteRenderer>() != null){
            spriteRenderer = GetComponent<SpriteRenderer>();
            objectHeight = spriteRenderer.bounds.max.y - spriteRenderer.bounds.min.y;
        }
        else if(GetComponent<TilemapRenderer>() != null){
            tilemapRenderer = GetComponent<TilemapRenderer>();
            objectHeight = tilemapRenderer.bounds.max.y - tilemapRenderer.bounds.min.y;
        }
        
        cam = FindObjectOfType<Camera>();
        camHeight = cam.orthographicSize*2f;


        amountOfBrothers = transform.parent.transform.GetComponentsInChildren<WaterfallScroll>().Length -1;
    }

    // Update is called once per frame
    void Update()
    {
        float dislocation = baseSpeed * Time.deltaTime * -1;
        transform.position = new Vector3(transform.position.x, transform.position.y + dislocation);

        float currentObjectY  = transform.position.y;
        float currentCamY = cam.transform.position.y;
        
        if(currentObjectY + (objectHeight*((float)amountOfBrothers+0.5f)) <= currentCamY + camHeight)
            PutOnTopOfLine();
    }
    

    void PutOnTopOfLine(){
        transform.position = new Vector3(transform.position.x, transform.position.y + objectHeight*((float)amountOfBrothers+1f));
    }
}
