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
    private string fase1acertos = ScoreManager.level01HighScore.ToString();
    private string fase2acertos = ScoreManager.level02HighScore.ToString();
    private string fase3acertos = ScoreManager.level03HighScore.ToString();
    private string somaDeAcertos = (ScoreManager.level01HighScore + ScoreManager.level02HighScore + ScoreManager.level03HighScore).ToString();


    void Start()
    {
        totalDeAcertos.text = somaDeAcertos;
        acertosFase1.text = fase1acertos;
        acertosFase2.text = fase2acertos;
        acertosFase3.text = fase3acertos;
    }
}
