using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : BaseSplash<SplashScreen>
{
    protected override ILoad GetLoader()
    {
        return SceneLoader.Instance;
    }

    protected override IMain GetMain()
    {
        return GameMain.Instance;
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
