using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] SceneController    sceneController;
    [SerializeField] CameraController   cameraController;
    [SerializeField] GameManager        gameManager;
    [SerializeField] BlockCrafter       blockCrafter;
    [SerializeField] ScoreManager       scoreManager;
    [SerializeField] GameConfig         gameConfig;
    [SerializeField] ColorManager       colorManager;
    [SerializeField] UIController       uIController;

    public override void InstallBindings()
    {
        Container.BindInstance(sceneController).AsSingle();
        Container.BindInstance(blockCrafter).AsSingle();
        Container.BindInstance(cameraController).AsSingle();
        Container.BindInstance(gameManager).AsSingle();
        Container.BindInstance(scoreManager).AsSingle();
        Container.BindInstance(gameConfig).AsSingle();
        Container.BindInstance(colorManager).AsSingle();
        Container.BindInstance(uIController).AsSingle();
    }

}
