using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameLib
{
    public static bool DetectCharacter(Camera sight, CharacterController cc)
    {
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        return GeometryUtility.TestPlanesAABB(ps, cc.bounds);
    }
    
    public static void JJRotate(Transform transform, Vector3 destination, CharacterStat stat)
    {
        // B-A는 A에서 B로 향하는 벡터.
        Vector3 dir = destination - transform.position;
        dir.y = 0.0f;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    Quaternion.LookRotation(dir),
                    stat.turnSpeed * Time.deltaTime
                );
        }
    }

    public static void JJMove(CharacterController cc, Vector3 destination, CharacterStat stat)
    {
        Transform transform = cc.transform;

        JJRotate(transform, destination, stat);

        Vector3 deltaMove = Vector3.MoveTowards(
            transform.position,
            destination,
            stat.moveSpeed * Time.deltaTime) - transform.position;

        deltaMove.y = -stat.fallSpeed * Time.deltaTime;

        cc.Move(deltaMove);
    }

    public static void JJMove(CharacterController cc, Transform target, CharacterStat stat)
    {
        Transform transform = cc.transform;
        JJMove(cc, target.position, stat);
    }
}
