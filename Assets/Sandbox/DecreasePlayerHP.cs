using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class DecreasePlayerHP : MonoBehaviour
{
    [Inject] private FloatVariable _playerHp = default;
    //[Inject(Id = "PlayerHPChangedEvent")] private GameEvent _playerHPChanged = default;

    //private void Start()
    //{
    //    var clickStream = this.UpdateAsObservable().Where(_ => Mouse.current.leftButton.wasPressedThisFrame).Subscribe(_ => TakeIt());
    //}

    //private void TakeIt()
    //{

    //    _playerHp.RuntimeValue.Value -= 1.0f;
    //}

}
