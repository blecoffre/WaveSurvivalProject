using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class DisplayPlayerHP : MonoBehaviour
{
    [Inject(Id = "PlayerHPChangedEvent")] private GameEvent _playerHPChanged = default;
    private TextMeshProUGUI _hpText = default;
    private GameEventListener _listener = default;
    [Inject(Id = "PlayerHPVariable")] private FloatVariable _playerHp = default;

    private void Start()
    {
        _hpText = GetComponent<TextMeshProUGUI>();
        _listener = gameObject.AddComponent<GameEventListener>();
        _listener.Event = _playerHPChanged;
        _listener.Response = new UnityEngine.Events.UnityEvent();
        _listener.Response.AddListener(UpdateText);
        _listener.OnEnable();
    }

    private void UpdateText()
    {
        _hpText?.SetText(_playerHp.RuntimeValue.ToString("F2"));
    }
}
