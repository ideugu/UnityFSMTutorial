using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    public FSMManager manager;

    private void Awake()
    {
        manager = transform.root.GetComponent<FSMManager>();
    }

    void AttackHitCheck()
    {
        manager.AttackCheck();
    }

}
