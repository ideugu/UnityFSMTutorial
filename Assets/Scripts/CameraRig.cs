using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public bool useLag;
    public float lagSpeed = 2;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update ()
    {
        if(!useLag)
        {
            transform.position = player.position + offset;
        }
        else
        {
            Vector3 target = player.position + offset;
            transform.position
                = new Vector3(transform.position.x, target.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, 
                target, lagSpeed * Time.deltaTime);
        }
    }
}
