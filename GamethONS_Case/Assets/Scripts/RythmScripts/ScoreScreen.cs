using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtAcertos;
    [SerializeField] private TextMeshProUGUI txtPrecisao;
    [SerializeField] private TextMeshProUGUI txtBonusVida;
    [SerializeField] private TextMeshProUGUI txtConcertos;

    [SerializeField] private TextMeshProUGUI txtPontuacao;


    void Awake()
    {
        txtAcertos.text = ScoreManager.Instance.levelHits.ToString();
        txtPrecisao.text = (Mathf.Round(ScoreManager.Instance.levelAccuracy*100*100)/100).ToString() + "%";
        txtBonusVida.text = ScoreManager.Instance.levelRecoveredHealth.ToString();
        txtConcertos.text = ScoreManager.Instance.levelCompletedEncounters.ToString() + " / " + ScoreManager.Instance.totalEncountersInAllLevels;
        
        txtPontuacao.text = ScoreManager.currentLevelScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            ScenesManager.instance.PlayScene("HubCentral", true);
        }
    }
}
