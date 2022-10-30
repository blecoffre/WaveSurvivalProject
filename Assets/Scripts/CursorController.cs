using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class CursorController : MonoBehaviour
{
    [Inject] private DebugController _debugController = default;
    [Inject] private ShopsManager _shopsManager = default;

    private void Start()
    {
        LockCursor();
#if UNITY_EDITOR
        _debugController.WindowIsVisible.Subscribe(x => CursorState(x));
#endif

        _shopsManager.OnOpenShop.Subscribe(_ =>
        {
            CursorState(true);
        });

        _shopsManager.OnCloseShop.Subscribe(_ =>
        {
            CursorState(false);
        });
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
