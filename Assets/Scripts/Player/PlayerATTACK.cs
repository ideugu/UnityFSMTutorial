using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATTACK : PlayerFSMState
{
    public override void BeginState()
    {
        base.BeginState();
        manager.marker.gameObject.SetActive(false);
        manager.attackMarker.gameObject.SetActive(true);
    }

    void Update ()
    {
        if(Vector3.Distance(transform.position, manager.target.position) 
            > manager.stat.attackRange)
        {
            manager.SetState(PlayerState.CHASE);
            return;
        }
        GameLib.JJRotate(transform, manager.target.position, manager.stat);

    }
}
