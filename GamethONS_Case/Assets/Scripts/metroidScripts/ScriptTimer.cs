using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTimer : MonoBehaviour
{
    public float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
}
