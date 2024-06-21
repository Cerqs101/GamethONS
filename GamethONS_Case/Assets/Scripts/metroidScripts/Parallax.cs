using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 0f;
    private float camWidth;
    private float objectWidth;
    private int amountOfBrothers;

    private Camera cam;
    private SpriteRenderer spriteRenderer;
    private TilemapRenderer tilemapRenderer;


    // Start is called before the first frame update
    void Awake()
    {
        if(GetComponent<SpriteRenderer>() != null){
            spriteRenderer = GetComponent<SpriteRenderer>();
            objectWidth = spriteRenderer.bounds.max.x - spriteRenderer.bounds.min.x;

            if(baseSpeed == 0f)
                baseSpeed = 0.75f/((float)spriteRenderer.sortingOrder*-1f);
        }
        else if(GetComponent<TilemapRenderer>() != null){
            tilemapRenderer = GetComponent<TilemapRenderer>();
            objectWidth = tilemapRenderer.bounds.max.x - tilemapRenderer.bounds.min.x;

            if(baseSpeed == 0f)
                baseSpeed = 0.75f/((float)tilemapRenderer.sortingOrder*-1f);
        }
        
        cam = FindObjectOfType<Camera>();
        camWidth = cam.aspect * cam.orthographicSize*2f;


        amountOfBrothers = transform.parent.transform.GetComponentsInChildren<Parallax>().Count()-1;
    }


    // Update is called once per frame
    void Update()
    {
        float cameraXSpeed = cam.velocity.x;

        if(Mathf.Abs(cameraXSpeed) > 0)
        {
            float dislocation = cameraXSpeed * baseSpeed * Time.deltaTime * -1;
            SetXPosition(transform.position.x + dislocation);
        }

        float currentObjectX  = transform.position.x;
        float currentCamX = cam.transform.position.x;
        if(currentObjectX - (objectWidth*((float)amountOfBrothers+1)) >= currentCamX - camWidth)
            PutOnLeftEdgeOfLine();
        else if(currentObjectX + (objectWidth*((float)amountOfBrothers+1)) <= currentCamX + camWidth)
            PutOnRightEdgeOfLine();
    }


    void PutOnLeftEdgeOfLine(){
        SetXPosition(transform.position.x - objectWidth*((float)amountOfBrothers+1f));
    }
    void PutOnRightEdgeOfLine(){
        SetXPosition(transform.position.x + objectWidth*((float)amountOfBrothers+1f));
    }


    void SetXPosition(float newXPosition)
    {
        transform.position = new Vector3(newXPosition, transform.position.y);
    }
}
