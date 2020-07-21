using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeState
{
    IDLE = 0,
    PATROL,
    CHASE,
    ATTACK,
    DEAD
}

public class SlimeFSMManager : MonoBehaviour, IFSMManager
{
    public SlimeState currentState;
    public SlimeState startState;

    Dictionary<SlimeState, SlimeFSMState> states 
        = new Dictionary<SlimeState, SlimeFSMState>();

    public Animator anim;
    public CharacterController cc;
    public CharacterStat stat;
    public Camera sight;
    public CharacterController playerCC;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        cc = GetComponent<CharacterController>();
        stat = GetComponent<CharacterStat>();
        sight = GetComponentInChildren<Camera>();
        playerCC = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<CharacterController>();

        states.Add(SlimeState.IDLE, GetComponent<SlimeIDLE>());
        states.Add(SlimeState.PATROL, GetComponent<SlimePATROL>());
        states.Add(SlimeState.CHASE, GetComponent<SlimeCHASE>());
        states.Add(SlimeState.ATTACK, GetComponent<SlimeATTACK>());
        states.Add(SlimeState.DEAD, GetComponent<SlimeDEAD>());
    }

    private void Start()
    {
        SetState(startState);
    }

    public void SetState(SlimeState newState)
    {
        if (currentState == SlimeState.DEAD)
            return;

        foreach (SlimeFSMState state in states.Values)
        {
            state.enabled = false;
        }

        currentState = newState;
        states[currentState].enabled = true;
        states[currentState].BeginState();
        anim.SetInteger("CurrentState", (int)currentState);
    }

    private void OnDrawGizmos()
    {
        if(sight != null)
        {
            Gizmos.color = Color.red;
            Matrix4x4 temp = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.TRS(
                sight.transform.position, sight.transform.rotation, Vector3.one );

            Gizmos.DrawFrustum(
                Vector3.zero, sight.fieldOfView,
                sight.farClipPlane, sight.nearClipPlane, sight.aspect );

            Gizmos.matrix = temp;
        }
    }

    public void AttackCheck()
    {
        if (currentState != SlimeState.ATTACK)
            return;

        CharacterStat targetStat
            = playerCC.GetComponent<CharacterStat>();

        targetStat.ApplyDamage(stat);
    }

    public void SetDead()
    {
        SetState(SlimeState.DEAD);
    }

    public void NotifyDead()
    {
        SetState(SlimeState.IDLE);
        playerCC = null;
    }
}
