using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickGameModel : BaseModel, IClickGameModel
{
    public int Coin { get; private set; }

    public void SetCoin(int coin)
    {
        Coin = coin;
        SetDataAsDirty();
    }

    public void AddCoin()
    {
        Coin++;
        SetDataAsDirty();
    }

    public void SubstractCoin()
    {
        Coin--;
        SetDataAsDirty();
    }
}
