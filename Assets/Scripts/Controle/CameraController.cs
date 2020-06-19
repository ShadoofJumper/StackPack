using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float perspectiveSizeK = 0.33f;
    [SerializeField] private float growthTopStep;
    [SerializeField] private float growthSpeed;
    [SerializeField] private float moveAwaySpeed;
    [SerializeField] private float moveAwayOffset;
    [SerializeField] private GameObject background;
    private SceneController sceneController;
    private Camera          cameraComponent;
    private GameManager     gameManager;
    private Vector3         cameraOffset;
    private static Vector3  gameEndCameraPosition;
    private float levelHeight;

    [Inject]
    private void Constructor(SceneController sceneController, GameManager gameManager, GameConfig gameConfig)
    {
        this.sceneController    = sceneController;
        this.gameManager        = gameManager;
        levelHeight             = gameConfig.LevelHeight;
    }

    private void Awake()
    {
        cameraOffset    = transform.position;
        cameraComponent = GetComponent<Camera>();
    }

    public void MoveCameraAway(UnityAction callback = null)
    {
        Vector3 moveToPosition  = transform.position + Vector3.up * (moveAwayOffset + gameManager.CurrentLevel * levelHeight);
        gameEndCameraPosition = moveToPosition;
        transform.DOMove(moveToPosition, moveAwaySpeed, false).OnComplete(() => callback.Invoke());
    }

    public void MoveCameraToStart(UnityAction callback = null)
    {
        transform.position = gameEndCameraPosition;
        transform.DOMove(cameraOffset, moveAwaySpeed, false).OnComplete(() => callback.Invoke()); ;
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
        float scaleFactor           = targertScale / cameraComponent.orthographicSize;
        Vector3 targetPosition      = cameraOffset + Vector3.up * (gameManager.CurrentLevel * levelHeight) / 2;
        Vector3 bgScaleTarget       = background.transform.localScale * scaleFactor;

        //for camera
        cameraComponent.DOOrthoSize (targertScale, growthSpeed);
        transform.DOMove(targetPosition, growthSpeed, false);
        //fpr bg
        background.transform.DOScale(bgScaleTarget, growthSpeed);
    }

}
