using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fillBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public Color32 errorColor = new Color32(207,38,53,255);
    [SerializeField]public Color32 goodColor = new Color32(207,38,53,255);
    [SerializeField]public Color32 perfectColor = new Color32(207,38,53,255);
    public Image imagen;
    public static fillBar Instance;
    void Start()
    {
        Instance = this;
        imagen = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
    }

}
