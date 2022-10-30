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
    private RaycastHit _raycastHit;


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
        Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.red);
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out _raycastHit, _playerInteractionDistance.Value + dist))
        {
            IInteractable interactable = _raycastHit.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log(_raycastHit.transform.gameObject.name);
            }
            return interactable;
        }

        return null;
    }

    public async void Interact()
    {
        if (_interactableObject != null)
        {
            bool hasInterracted = await _interactableObject.Value.Interact();

            if (hasInterracted)
            {
                _contextDisplay.ShowInteractionTip(false);
            }
        }
    }
}
