using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretShopSlotView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name = default;
    [SerializeField] private Image _icon = default;
    [SerializeField] private ButtonManager _buyButton = default;

    public void Initialize(BaseTurretShopSlotData data)
    {
        _name.SetText(data.Name.RuntimeValue.Value);
        _icon.sprite = data.SlotIcon;
        _buyButton.buttonText = data.Price.RuntimeValue.Value.ToString();
    }
}
