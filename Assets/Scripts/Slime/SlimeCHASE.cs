using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCHASE : SlimeFSMState
{
    public override void BeginState()
    {
        base.BeginState();
    }

    void Update ()
    {
        if (!GameLib.DetectCharacter(manager.sight, manager.playerCC))
        {
            manager.SetState(SlimeState.IDLE);
            return;
        }

        if(Vector3.Distance(
            transform.position, 
            manager.playerCC.transform.position) <= manager.stat.attackRange)
        {
            manager.SetState(SlimeState.ATTACK);
            return;
        }
        
        GameLib.JJMove(manager.cc,
            manager.playerCC.transform,
            manager.stat);
	}
}
