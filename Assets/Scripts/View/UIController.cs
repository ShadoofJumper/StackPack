using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private Text       currentScore;
    [SerializeField] private Text       bestScoreMenuText;
    [SerializeField] private Text       restartText;
    private float showUIDelay = 0.5f;

    // ------------------- Panel Access ---------------------
    // ------------------- Menu Access ---------------------
    public void ShowMenuPanel(bool fade = false)
    {
        menuPanel.SetActive(true);
        if (fade)
            ChangeFadeAllTextChild(menuPanel, true);
    }

    public void HideMenuPanel(bool fade = false)
    {
        HidePanel(menuPanel, fade);
    }
    // ------------------- In game UI Access ---------------------
    public void ShowInGamePanel(bool fade = false)
    {
        inGamePanel.SetActive(true);
        if (fade)
            ChangeFadeAllTextChild(inGamePanel, true);
    }

    public void HideInGamePanel(bool fade = false)
    {
        HidePanel(inGamePanel, fade);
    }

    public void HideCurrentScore(bool fade)
    {
        if (fade)
        {
            HideTextInFade(currentScore);
        }
        else
        {
            currentScore.text = "";
        }
    }
    // ------------------- In Restart UI Access ---------------------
    public void ShowRestartPanel(bool fade = false)
    {
        restartPanel.SetActive(true);
        if (fade)
            ChangeFadeAllTextChild(restartPanel, true);
    }

    public void HideRestartPanel(bool fade = false)
    {
        HidePanel(restartPanel, fade);
    }


    private void HidePanel(GameObject panel, bool fade)
    {
        if (fade)
        {
            ChangeFadeAllTextChild(panel, false, delegate {
                panel.SetActive(false);
            });
        }
        else
        {
            panel.SetActive(false);
        }
    }
    // -----------------------------------
    public void UpdateCurrentScore(int score = 0)
    {
        currentScore.text = score == 0 ? "" : score.ToString();
        if (score == 1)
            ShowTextFromFade(currentScore);
    }

    public void UpdateBestScore(int score)
    {
        bestScoreMenuText.text = "RECORD: " + score;
    }

    public void UpdateRestartText(string text)
    {
        restartText.text = text;
    }

    // ---------------- opacity fade logic ------------------
    private void ChangeFadeAllTextChild(GameObject gameObject, bool isShow, UnityAction callback = null)
    {
        foreach (Text text in gameObject.GetComponentsInChildren<Text>())
        {
            if (isShow)
            {
                ShowTextFromFade(text);
            }
            else
            {
                HideTextInFade(text);
            }
        }
        if(callback!=null)
        StartCoroutine(ActionAfterFade(callback, showUIDelay));
    }

    IEnumerator ActionAfterFade(UnityAction callback, float delay)
    {
        yield return new WaitForSeconds(delay);
        callback.Invoke();
    }

    private void ShowTextFromFade(Text text)
    {
        Color targetColor   = text.color;
        Color startColor    = targetColor;
        startColor.a        = 0.0f;
        text.color          = startColor;
        text.DOColor(targetColor, showUIDelay);
    }

    private void HideTextInFade(Text text)
    {
        Color targetColor   = text.color;
        targetColor.a       = 0.0f;
        text.DOColor(targetColor, showUIDelay);
    }

}
