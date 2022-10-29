using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class PlayerInventoryFeedback : MonoBehaviour
{
    [SerializeField] private MMFeedbacks _moneyFeedback = default;

    [Inject] private PlayerInventoryController _inventory = default;
    [Inject] private PlayerPortraitInfos _moneyView = default;

    private void Start()
    {
        SetupFeedback();
    }

    private void SetupFeedback()
    {
        _inventory.PlayerGain.Skip(1).Subscribe(x => PlayMoneyFeedback(x));
    }

    private void PlayMoneyFeedback(int gain)
    {
        _moneyFeedback.PlayFeedbacks(_moneyView._moneyFeedbackPosition.position, gain);
    }
}
