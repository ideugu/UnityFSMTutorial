using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePATROL : SlimeFSMState
{
    public Vector3 destination;

    public override void BeginState()
    {
        base.BeginState();
        destination = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }

    void Update ()
    {
        if((manager.playerCC != null) && GameLib.DetectCharacter(manager.sight,manager.playerCC))
        {
            manager.SetState(SlimeState.CHASE);
            return;
        }

        Vector3 diff = destination - transform.position;
        diff.y = 0;
        if(diff.magnitude < 0.1f)
        {
            manager.SetState(SlimeState.IDLE);
            return;
        }

        GameLib.JJMove(manager.cc, destination, manager.stat);
	}
}
