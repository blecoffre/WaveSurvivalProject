using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerPortraitInfos : MonoBehaviour
{
    [SerializeField] private Image _playerIcon = default;
    [SerializeField] private TextMeshProUGUI _playerName = default;
    [SerializeField] private TextMeshProUGUI _playerMoney = default;
    public Transform _moneyFeedbackPosition = default;

    public void SetIconAndName(Sprite icon, string name)
    {
        _playerIcon.sprite = icon;
        _playerName.SetText(name);
    }

    public void UpdateMoney(int amount)
    {
        _playerMoney.SetText(amount.ToString());
    }
}
