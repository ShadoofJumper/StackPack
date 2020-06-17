using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] SceneController    sceneController;
    [SerializeField] BlockCrafter       blockCrafter;

    public override void InstallBindings()
    {
        Container.BindInstance(sceneController).AsSingle();
        Container.BindInstance(blockCrafter).AsSingle();
    }

}
