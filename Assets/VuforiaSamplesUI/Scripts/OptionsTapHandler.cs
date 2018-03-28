/*============================================================================== 
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.   
==============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsTapHandler : TapHandler
{
    OptionsConfig optionsConfig;

    void Awake()
    {
        optionsConfig = FindObjectOfType<OptionsConfig>();
    }

    protected override void OnDoubleTap()
    {
        if (optionsConfig.AnyOptionsEnabled())
            base.OnDoubleTap();
    }
}
