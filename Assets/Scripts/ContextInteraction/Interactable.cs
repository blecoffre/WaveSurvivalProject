using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public virtual void Interact()
    {
#if UNITY_EDITOR
        Debug.Log("Interact with " + gameObject.name);
#endif
    }
}
