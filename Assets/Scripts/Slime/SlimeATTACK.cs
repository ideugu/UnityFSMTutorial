using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeATTACK : SlimeFSMState
{
    public override void BeginState()
    {
        base.BeginState();
    }

    void Update ()
    {
        if (Vector3.Distance(
            transform.position,
            manager.playerCC.transform.position) > manager.stat.attackRange)
        {
            manager.SetState(SlimeState.CHASE);
            return;
        }

        GameLib.JJRotate(transform, manager.playerCC.transform.position, manager.stat);

    }
}
