using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRUN : PlayerFSMState
{
    public override void BeginState()
    {
        base.BeginState();
        manager.marker.gameObject.SetActive(true);
        manager.attackMarker.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameLib.JJMove(manager.cc, manager.marker, manager.stat);

        Vector3 diff = manager.marker.position - transform.position;
        diff.y = 0.0f;
        if(diff.magnitude < 0.1f)
        {
            manager.SetState(PlayerState.IDLE);
        }
    }
}
