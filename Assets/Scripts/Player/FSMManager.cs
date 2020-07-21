﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE = 0,
    RUN,
    CHASE,
    ATTACK,
    DEAD
}

public class FSMManager : MonoBehaviour, IFSMManager
{

    public PlayerState currentState;
    public PlayerState startState;
    public Transform marker;
    public Transform attackMarker;
    public Transform target;
    public Animator anim;
    public CharacterStat stat;
    public CharacterController cc;
    public int layerMask;


    Dictionary<PlayerState, PlayerFSMState> states = new Dictionary<PlayerState, PlayerFSMState>();

    private void Awake()
    {
        layerMask = (1 << 9) + (1 << 10);

        marker = GameObject.FindGameObjectWithTag("Marker").transform;
        attackMarker = GameObject.FindGameObjectWithTag("AttackMarker").transform;
        anim = GetComponentInChildren<Animator>();
        stat = GetComponent<CharacterStat>();
        cc = GetComponent<CharacterController>();

        states.Add(PlayerState.IDLE,GetComponent<PlayerIDLE>());
        states.Add(PlayerState.RUN, GetComponent<PlayerRUN>());
        states.Add(PlayerState.CHASE, GetComponent<PlayerCHASE>());
        states.Add(PlayerState.ATTACK, GetComponent<PlayerATTACK>());
        states.Add(PlayerState.DEAD, GetComponent<PlayerDEAD>());
    }

    public void SetState(PlayerState newState)
    {
        if (currentState == PlayerState.DEAD)
            return;

        foreach(PlayerFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }

        states[newState].enabled = true;
        states[newState].BeginState();
        currentState = newState;
        anim.SetInteger("CurrentState", (int)currentState);
    }

    // Use this for initialization
    void Start ()
    {
        SetState(startState);
    }

    // Update is called once per frame
    void Update ()
    {
        if (currentState == PlayerState.DEAD)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(r, out hit, 1000, layerMask))
            {
                if(hit.transform.gameObject.layer == 9)
                {
                    marker.position = hit.point;
                    target = null;
                    SetState(PlayerState.RUN);
                }
                else if(hit.transform.gameObject.layer == 10)
                {
                    target = hit.transform;
                    attackMarker.parent = hit.transform;
                    attackMarker.localPosition = Vector3.zero;
                    SetState(PlayerState.CHASE);
                }
            }
        }
	}

    public void AttackCheck()
    {
        if (currentState != PlayerState.ATTACK)
            return;

        CharacterStat targetStat 
            = target.GetComponent<CharacterStat>();

        targetStat.ApplyDamage(stat);
    }

    public void SetDead()
    {
        SetState(PlayerState.DEAD);
    }

    public void NotifyDead()
    {
        SetState(PlayerState.IDLE);
        attackMarker.parent = null;
    }
}
