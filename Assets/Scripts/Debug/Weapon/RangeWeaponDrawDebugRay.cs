using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponDrawDebugRay : MonoBehaviour
{
    private LineRenderer _line = default;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    public void DrawRay(Vector3[] pos, float destroyTime)
    {
        _line.SetPositions(pos);
        DestroyAfterTime(destroyTime);
    }

    private void DestroyAfterTime(float time)
    {
        Destroy(gameObject, time);
    }
}
