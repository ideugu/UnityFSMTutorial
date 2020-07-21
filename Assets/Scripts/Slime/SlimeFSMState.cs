using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFSMState : MonoBehaviour
{
    public SlimeFSMManager manager;

    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<SlimeFSMManager>();
    }

}
