using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DebugControls))]
public class DebugController : MonoBehaviour
{
    [SerializeField] private DebugWindow _debugWindowPrefab = default;

    private DebugControls _controls = default;
    private DebugWindow _debugWindowInstance = default;

    private void Awake()
    {
        _controls = GetComponent<DebugControls>();
    }

    private void AddDebugWindowToScene()
    {
        if(_debugWindowInstance is null)
        {
            _debugWindowInstance = Instantiate(_debugWindowPrefab);
        }
    }
}
