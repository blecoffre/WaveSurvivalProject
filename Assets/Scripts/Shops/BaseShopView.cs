using Michsky.MUIP;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BaseShopView : MonoBehaviour, IOpenable, IClosable
{
    [SerializeField] private TextMeshProUGUI _title = default;
    [SerializeField] private ButtonManager _closeShopButton = default;

    [Inject] private ShopsManager _shopsManager = default;

    public void Initialize(BaseShopDatas shopDatas)
    {
        _title.SetText(shopDatas.ShopName);
        _closeShopButton.onClick.AddListener(() => TryCloseShop());
    }

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

        Destroy(gameObject);
    }

    private void TryCloseShop()
    {
        _shopsManager.CloseShop(this);
    }
}
