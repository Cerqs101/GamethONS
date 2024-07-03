using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalDeAcertos;
    [SerializeField] private TextMeshProUGUI acertosFase1;
    [SerializeField] private TextMeshProUGUI acertosFase2;
    [SerializeField] private TextMeshProUGUI acertosFase3;
    private int fase1acertos; //ScoreManager.level01HighScore.ToString();
    private int fase2acertos;//ScoreManager.level02HighScore.ToString();
    private int fase3acertos;//ScoreManager.level03HighScore.ToString();

    
    void Start()
    {
        fase1acertos = SaveSystem.GetHighScore("Fase1");
        fase2acertos = SaveSystem.GetHighScore("Fase2");
        fase3acertos = SaveSystem.GetHighScore("Fase3");

        totalDeAcertos.text = (fase1acertos+fase2acertos+fase3acertos).ToString();
        acertosFase1.text = fase1acertos.ToString();
        acertosFase2.text = fase2acertos.ToString();
        acertosFase3.text = fase3acertos.ToString();
    }
}
