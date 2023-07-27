using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameLevelController>().FromNew().AsSingle().NonLazy();
    }
}
