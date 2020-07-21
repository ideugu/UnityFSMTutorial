using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIDLE : SlimeFSMState
{
    public float idleTime = 3.0f;
    public float elapsedTime = 0;

    public override void BeginState()
    {
        base.BeginState();
        elapsedTime = 0;
    }

    void Update ()
    {
        if ((manager.playerCC != null) && GameLib.DetectCharacter(manager.sight, manager.playerCC))
        {
            manager.SetState(SlimeState.CHASE);
            return;
        }

        elapsedTime += Time.deltaTime;
        if(elapsedTime >= idleTime)
        {
            manager.SetState(SlimeState.PATROL);
        }
		
	}
}
