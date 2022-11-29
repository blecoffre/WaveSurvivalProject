using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Utils : MonoBehaviour
{
    public static Vector3 PredictV3Pos(Vector3 muzzlePos, float bulletVelocity, Vector3 targetPos, Vector3 targetVelocity, bool useGravity = false)
    {
        Vector3 totarget = targetPos - muzzlePos;

        float a = Vector3.Dot(targetVelocity, targetVelocity) - (bulletVelocity * bulletVelocity);

        if (a == 0F) { Debug.Log("wasd"); }
        if (bulletVelocity == 0F) { Debug.Log("bbb"); }

        float b = 2 * Vector3.Dot(targetVelocity, totarget);
        float c = Vector3.Dot(totarget, totarget);

        float p = -b / (2 * a);
        float q = (float)Mathf.Sqrt((b * b) - 4 * a * c) / (2 * a);

        float t1 = p - q;
        float t2 = p + q;
        float t;

        if (t1 > t2 && t2 > 0)
        {
            t = t2;
        }
        else
        {
            t = t1;
        }

        Vector3 aimSpot = targetPos + targetVelocity * t;
        Vector3 bulletPath = aimSpot - muzzlePos;

        //if no drag
        if (useGravity)
        {
            float timeToImpact = bulletPath.magnitude / bulletVelocity;//speed must be in units per second
            Vector3 gravityDrop = 0.5F * Physics.gravity * timeToImpact * timeToImpact; //1/2gt^2
            return aimSpot - gravityDrop;
        }

        return aimSpot;
    }
}
