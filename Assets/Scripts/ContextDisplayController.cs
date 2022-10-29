using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextDisplayController : MonoBehaviour
{
    [SerializeField] private GameObject _interactionTipDisplay = default;


    private void Awake()
    {
        FirstDisableAllContextDisplay();
    }

    private void FirstDisableAllContextDisplay()
    {
        _interactionTipDisplay.SetActive(false);
    }

    public void ShowInteractionTip(bool show)
    {
        Debug.Log("Show tip with value : " + show);
        _interactionTipDisplay.SetActive(show);
    }
}
