using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private Text       currentScore;
    [SerializeField] private Text       bestScoreMenu;
    [SerializeField] private Text       restartText;

    // ------- Panel Access --------
    public void ShowMenuPanel()
    {
        menuPanel.SetActive(true);
    }

    public void HideMenuPanel()
    {
        menuPanel.SetActive(false);
    }

    public void ShowInGamePanel()
    {
        inGamePanel.SetActive(true);
    }

    public void HideInGamePanel()
    {
        inGamePanel.SetActive(false);
    }

    public void ShowRestartPanel()
    {
        restartPanel.SetActive(true);
    }
    public void HideRestartPanel()
    {
        restartPanel.SetActive(false);
    }
    // -----------------------------------
    public void UpdateCurrentScore(int score = 0)
    {
        currentScore.text = score == 0 ? "" : score.ToString();
    }

    public void UpdateBestScore(int score)
    {
        bestScoreMenu.text = "RECORD: " + score;
    }

    public void UpdateRestartText(string text)
    {
        restartText.text = text;
    }

}
