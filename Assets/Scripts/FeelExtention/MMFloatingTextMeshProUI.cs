using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = System.Random;
using TMPro;
using MoreMountains.Tools;

public class MMFloatingTextMeshProUI : MMFloatingText
{
    [Header("TextMeshPro")]
    /// the TextMeshPro object to use to display values
    public TextMeshProUGUI TargetTextMeshPro;

    /// <summary>
    /// On init we grab our TMP's color
    /// </summary>
    protected override void Initialization()
    {
        base.Initialization();
        _initialTextColor = TargetTextMeshPro.color;
    }

    /// <summary>
    /// Sets the TMP's value
    /// </summary>
    /// <param name="newValue"></param>
    public override void SetText(string newValue)
    {
        TargetTextMeshPro.text = newValue;
    }

    /// <summary>
    /// Sets the color of the target TMP
    /// </summary>
    /// <param name="newColor"></param>
    public override void SetColor(Color newColor)
    {
        TargetTextMeshPro.color = newColor;
    }

    /// <summary>
    /// Sets the opacity of the target TMP
    /// </summary>
    /// <param name="newOpacity"></param>
    public override void SetOpacity(float newOpacity)
    {
        _newColor = TargetTextMeshPro.color;
        _newColor.a = newOpacity;
        TargetTextMeshPro.color = _newColor;
    }
}
