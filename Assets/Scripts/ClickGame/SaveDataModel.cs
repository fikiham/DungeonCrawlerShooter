using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataModel : BaseModel, ISaveDataModel
{
    public int Coin { get; private set; }

    public void SetCoinData(int coin)
    {
        Coin = coin;
        SetDataAsDirty();
    }
}
