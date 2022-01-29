using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWindow : MonoBehaviour
{
    [SerializeField] private GameObject _container = default;
    
    private void Hide()
    {
        _container.SetActive(false);
    }

    private void Show()
    {
        _container.SetActive(true);
    }

    private void Close()
    {
        //Disable all affected
        Destroy(gameObject);
    }
}
