using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayConnector : BaseConnector
{
    private SaveDataController _saveData;
    private SoundfxController _soundfx;

    public void OnUpdateCoin(UpdateCoinMessage message)
    {
        _saveData.OnUpdateCoin(message.Coin);
        _soundfx.OnUpdateCoin();
    }

    protected override void Connect()
    {
        Subscribe<UpdateCoinMessage>(OnUpdateCoin);
    }

    protected override void Disconnect()
    {
        Unsubscribe<UpdateCoinMessage>(OnUpdateCoin);
    }
}
