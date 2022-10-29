using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerInteractionController : MonoBehaviour
{
    private ReactiveProperty<float> _playerInteractionDistance;
    private Camera _mainCamera = default;
    private ReactiveProperty<IInteractable> _interactableObject = new ReactiveProperty<IInteractable>();

    [Inject] private ContextDisplayController _contextDisplay = default;

    [Inject]
    private void Init(PlayerLookData data)
    {
        _playerInteractionDistance = data.GetPlayerInteractionDistance();
    }

    private void Start()
    {
        _mainCamera = Camera.main;

        _interactableObject.ObserveEveryValueChanged(x => x.Value).Subscribe(x => _contextDisplay.ShowInteractionTip(x != null));

        Observable.EveryUpdate().Subscribe(_ =>
        {
            _interactableObject.Value = RayInteractWithObjectOfInterest();
        });
    }

    private IInteractable RayInteractWithObjectOfInterest()
    {
        float dist = Vector3.Distance(_mainCamera.transform.position, _mainCamera.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Follow.transform.position);
        RaycastHit hit;
        Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.red);
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, _playerInteractionDistance.Value + dist))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log(hit.transform.gameObject.name);
            }
            return interactable;
        }

        return null;
    }

    private void DrawRay()
    {

    }
}
