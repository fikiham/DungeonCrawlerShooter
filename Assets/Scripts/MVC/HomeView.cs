using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HomeView : BaseSceneView
{
    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private Button _quitButton;

    public void SetCallbacks(UnityAction onClickPlayButton, UnityAction onClickQuitButton)
    {
        _playButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();

        _playButton.onClick.AddListener(onClickPlayButton);
        _quitButton.onClick.AddListener(onClickQuitButton);
    }
}
