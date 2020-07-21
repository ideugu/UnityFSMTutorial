using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float turnSpeed = 540.0f;
    public float fallSpeed = 10.0f;
    public float attackRange = 2.0f;
    public float hp = 100;
    public float attackRate = 30;
    public float exp = 10;
    public CharacterStat lastHitBy = null;

    public IFSMManager manager;

    private void Awake()
    {
        manager = GetComponent<IFSMManager>();
    }

    public void NotifyDead()
    {
        manager.NotifyDead();
    }

    public void DropExp(CharacterStat to)
    {
        to.exp += exp;
    }

    public void ApplyDamage(CharacterStat from)
    {
        hp -= from.attackRate;
        if(hp <= 0)
        {
            manager.SetDead();
            if (lastHitBy == null)
            {
                lastHitBy = from;
                DropExp(lastHitBy);
            }

            from.NotifyDead();
        }
    }
}
