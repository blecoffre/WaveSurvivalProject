using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShop : MonoBehaviour, IOpenable, IClosable
{
    public virtual void Open()
    {
#if UNITY_EDITOR
        Debug.Log("Open Shop " + gameObject.name);
#endif
    }

    public virtual void Close()
    {
#if UNITY_EDITOR
        Debug.Log("Close Shop " + gameObject.name);
#endif
    }
}
