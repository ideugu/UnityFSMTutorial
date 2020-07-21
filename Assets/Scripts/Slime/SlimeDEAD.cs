using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDEAD : SlimeFSMState
{
    public override void BeginState()
    {
        base.BeginState();
        manager.cc.enabled = false;
        Destroy(gameObject, 5.0f);
    }

    void Update () {
		
	}
}
