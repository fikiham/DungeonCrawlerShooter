using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : BaseMain<GameMain>,IMain
{
    protected override IConnector[] GetConnectors()
    {
        return null;
    }

    protected override IController[] GetDependencies()
    {
        return new IController[]{
            new SaveDataController()
        };
    }

    protected override IEnumerator StartInit()
    {
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
