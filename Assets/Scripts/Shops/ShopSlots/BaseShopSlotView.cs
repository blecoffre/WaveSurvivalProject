using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BaseShopSlotView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name = default;
    [SerializeField] private Image _icon = default;
    [SerializeField] private ButtonManager _buyButton = default;

    protected SignalBus _signalBus;

    protected BaseShopSlotData _data;

    [Inject]
    public virtual void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public virtual void SetItemData(BaseShopSlotData data)
    {
        _name.SetText(data.Name.RuntimeValue.Value);
        _icon.sprite = data.SlotIcon;
        _buyButton.buttonText = data.Price.RuntimeValue.Value.ToString();
        _buyButton.onClick.AddListener(() =>
        {
            BuyItem();
        });

        _data = data;
    }

    public virtual void BuyItem()
    {
        _signalBus.AbstractFire(new TryBuyItemSignal() { Data = _data});
    }
}
