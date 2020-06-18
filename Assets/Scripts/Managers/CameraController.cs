using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float perspectiveSizeK = 0.33f;
    [SerializeField] private float growthTopStep;
    [SerializeField] private float growthSpeed;
    private SceneController sceneController;
    private Camera          cameraComponent;
    private GameManager     gameManager;
    private Vector3 cameraOffset;

    private float levelHeight;

    [Inject]
    private void Constructor(SceneController sceneController, GameManager gameManager, GameConfig gameConfig)
    {
        this.sceneController    = sceneController;
        this.gameManager        = gameManager;
        levelHeight             = gameConfig.LevelHeight;
    }

    private void Start()
    {
        cameraOffset    = transform.position;
        cameraComponent = GetComponent<Camera>();
    }

    public void MoveLevelUp()
    {
        Vector3 targetPosition = cameraOffset + sceneController.CurrentLevelCenter;
        transform.DOMove(targetPosition, growthSpeed, false);
    }

    public void LookPerspective()
    {
        float perspectiveScaleStep  = perspectiveSizeK * levelHeight * gameManager.CurrentLevel;
        float targertScale          = cameraComponent.orthographicSize + perspectiveScaleStep;
        Vector3 targetPosition      = cameraOffset + Vector3.up * (gameManager.CurrentLevel * 0.2f) / 2;

        cameraComponent.DOOrthoSize (targertScale, growthSpeed);
        transform.DOMove(targetPosition, growthSpeed, false);
    }

}
