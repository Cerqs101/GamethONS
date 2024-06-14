using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreditsRoll : MonoBehaviour
{
    [SerializeField] private float baseSpeed = -1f;
    [SerializeField] private float speedMultiplier = 4f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float dislocation = baseSpeed * Time.unscaledDeltaTime;
        if(Input.GetKey(KeyCode.UpArrow) && transform.position.y + dislocation*speedMultiplier <= 760)
        {
            dislocation *= speedMultiplier;
            transform.position = new Vector3(transform.position.x, transform.position.y + dislocation);
        }
        else if(Input.GetKey(KeyCode.DownArrow) && transform.position.y - dislocation*speedMultiplier >= 100)
        {
            dislocation *= -speedMultiplier;
            transform.position = new Vector3(transform.position.x, transform.position.y + dislocation);
        }
        else if(transform.position.y + dislocation*speedMultiplier <= 760)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + dislocation);
        }


    }
}
