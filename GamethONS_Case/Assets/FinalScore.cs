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
    private int fase1acertos = SaveSystem.Instance.GetHighScore("Fase1"); //ScoreManager.level01HighScore.ToString();
    private int fase2acertos = SaveSystem.Instance.GetHighScore("Fase2");//ScoreManager.level02HighScore.ToString();
    private int fase3acertos = SaveSystem.Instance.GetHighScore("Fase3");//ScoreManager.level03HighScore.ToString();


    void Start()
    {
        totalDeAcertos.text = (fase1acertos+fase2acertos+fase3acertos).ToString();
        acertosFase1.text = fase1acertos.ToString();
        acertosFase2.text = fase2acertos.ToString();
        acertosFase3.text = fase3acertos.ToString();

    }
}
