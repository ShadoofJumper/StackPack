using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private SceneController     sceneController;
    private CameraController    cameraController;
    private ScoreManager        scoreManager;
    private int currentLevel;
    private bool isGameInProgress;

    public int CurrentLevel         => currentLevel;
    public bool IsGameInProgress    => isGameInProgress;

    [Inject]
    private void Construct(SceneController sceneController, CameraController cameraController, ScoreManager scoreManager)
    {
        this.sceneController    = sceneController;
        this.cameraController   = cameraController;
        this.scoreManager       = scoreManager;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        isGameInProgress = true;
        sceneController.SpawnBaseBlock();
        sceneController.SpawnNewLevelBlock();
    }

    public void FailGame()
    {
        isGameInProgress = false;
        cameraController.LookPerspective();
    }

    public void NextStage()
    {
        currentLevel++;
        //move camera
        cameraController.MoveLevelUp();
        //add point to score
        scoreManager.AddScorePoint();
        //spawn next stage block
        sceneController.SpawnNewLevelBlock();
    }

}
