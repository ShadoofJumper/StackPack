using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreManager : MonoBehaviour
{
    private int totalScore;
    private UIController uIController;

    public int TotalScore => totalScore;

    [Inject]
    private void Construct(UIController uIController)
    {
        this.uIController = uIController;
    }

    public void AddScorePoint()
    {
        totalScore++;
        uIController.UpdateCurrentScore(totalScore);
    }

    public void SaveNewRecord()
    {
        int lastRecord = PlayerPrefs.GetInt("Record", 0);
        if (totalScore > lastRecord)
        {
            uIController.UpdateRestartText("NEW RECORD");
            uIController.UpdateBestScore(totalScore);
            PlayerPrefs.SetInt("Record", totalScore);
            return;
        }
        uIController.UpdateRestartText("RECORD: "+ lastRecord);
    }
}
