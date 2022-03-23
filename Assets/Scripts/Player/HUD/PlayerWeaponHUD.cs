using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWeaponHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentQuantityText = default;
    [SerializeField] private TextMeshProUGUI _maxQuantityText = default;

    public void UpdateAmmo(int currentQuantity, int maxQuantity)
    {
        _currentQuantityText.SetText(currentQuantity.ToString());
        _maxQuantityText.SetText(maxQuantity.ToString());
    }
}
