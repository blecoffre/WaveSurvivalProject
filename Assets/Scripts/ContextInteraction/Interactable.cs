using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public async virtual Task<bool> Interact()
    {
#if UNITY_EDITOR
        Debug.Log("Interact with " + gameObject.name);
#endif
        var b = await new UniTask<bool>(true);
        return b;
    }
}
