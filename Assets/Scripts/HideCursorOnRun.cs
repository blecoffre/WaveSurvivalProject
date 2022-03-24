using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class HideCursorOnRun : MonoBehaviour
{
    [Inject] private DebugController _debugController = default;

    private void Start()
    {
        LockCursor();
#if UNITY_EDITOR
        _debugController.WindowIsVisible.Subscribe(x => CursorState(x));
#endif
    }

    private void CursorState(bool isVisible)
    {
        if (isVisible)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
