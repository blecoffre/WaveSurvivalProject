using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class DisplayPlayerHP : MonoBehaviour
{
    [Inject(Id = "PlayerHPChangedEvent")] private GameEvent _playerHPChanged = default;
    private TextMeshProUGUI _hpText = default;
    private GameEventListener _listener = default;
    [Inject] private FloatVariable _playerHp = default;

    private void Start()
    {
        _hpText = GetComponent<TextMeshProUGUI>();
        //_listener = gameObject.AddComponent<GameEventListener>();
        //_listener.Event = _playerHPChanged;
        //_listener.Response = new UnityEngine.Events.UnityEvent();
        //_listener.Response.AddListener(UpdateText);
        //_listener.OnEnable();

        var clickStream = this.UpdateAsObservable().Where(_ => Mouse.current.leftButton.isPressed).Subscribe(_ => UpdateText());
    }

    private void UpdateText()
    {
        _hpText?.SetText(_playerHp.RuntimeValue.ToString("F2"));
    }
}
