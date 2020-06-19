using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    private SceneController     sceneController;
    private CameraController    cameraController;
    private ScoreManager        scoreManager;
    private UIController        uIController;
    private static  bool        isStartFirstTime    = true;
    private int     currentLevel;
    private bool    isGameInProgress;

    public int CurrentLevel         => currentLevel;
    public bool IsGameInProgress    => isGameInProgress;

    [Inject]
    private void Construct(SceneController sceneController, CameraController cameraController, ScoreManager scoreManager, UIController uIController)
    {
        this.sceneController    = sceneController;
        this.cameraController   = cameraController;
        this.scoreManager       = scoreManager;
        this.uIController       = uIController;
    }

    private void Start()
    {
        MoveCameraToStart();
        SetupStartView();
    }

    private void MoveCameraToStart()
    {
        if (!isStartFirstTime)
        {
            uIController.ShowMenuPanel(true);
            cameraController.MoveCameraToStart();
        } 
        isStartFirstTime = false;
    }

    private void SetupStartView()
    {
        sceneController.SpawnBaseBlock();
        uIController.UpdateBestScore(PlayerPrefs.GetInt("Record", 0));
    }


    // ------------ game progress loggic ------------
    public void StartGame()
    {
        isGameInProgress = true;
        sceneController.SpawnNewLevelBlock();
        uIController.HideMenuPanel(true);
        uIController.ShowInGamePanel(true);
        uIController.UpdateCurrentScore();
    }

    public void FailGame()
    {
        isGameInProgress = false;
        cameraController.LookPerspective();
        uIController.ShowRestartPanel(true);
        scoreManager.SaveNewRecord();
    }

    public void RestartGame()
    {
        uIController.HideCurrentScore(true);
        uIController.HideRestartPanel(true);
        cameraController.MoveCameraAway(delegate {
            SceneManager.LoadScene("Game");
        });
    }

    public void NextStage()
    {
        currentLevel++;
        cameraController.MoveLevelUp();
        scoreManager.AddScorePoint();
        sceneController.SpawnNewLevelBlock();
        //add light vibration
        Handheld.Vibrate();
    }
    // ----------------------------------------------

}
