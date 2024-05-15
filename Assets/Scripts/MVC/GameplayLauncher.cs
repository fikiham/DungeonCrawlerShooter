using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLauncher : SceneLauncher<GameplayLauncher, GameplayView>
{
    private ClickGameController _clickGame;

    // Use the same name with the scene that we add in build setting
    public override string SceneName => "Gameplay";

    protected override IConnector[] GetSceneConnectors()
    {
        return new IConnector[]{
            new GameplayConnector()
        };
    }

    protected override IController[] GetSceneDependencies()
    {
        return new IController[]{
            new ClickGameController(),
            new SoundfxController()
        };
    }

    protected override IEnumerator InitSceneObject()
    {
        _clickGame.SetView(_view.ClickGameView);
        yield return null;
    }

    protected override IEnumerator LaunchScene()
    {
        yield return null;
    }
}
