using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeLauncher : SceneLauncher<HomeLauncher, HomeView>
{
    // Use the same name with the scene that we add in build setting
    public override string SceneName => "Home";

    protected override IConnector[] GetSceneConnectors()
    {
        return null;
    }

    protected override IController[] GetSceneDependencies()
    {
        return null;
    }

    protected override IEnumerator InitSceneObject()
    {
        _view.SetCallbacks(OnClickPlayButton, OnClickQuitButton);
        yield return null;
    }

    protected override IEnumerator LaunchScene()
    {
        yield return null;
    }

    private void OnClickPlayButton()
    {
        SceneLoader.Instance.LoadScene("Gameplay");
    }

    private void OnClickQuitButton()
    {
        Application.Quit();
        print("QUITTING GAME");
    }
}
