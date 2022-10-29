using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAnimatorController : MonoBehaviour
{
    [Inject] private Animator _animator = default;

    private int _horizontal = default;
    private int _vertical = default;

    private void Awake()
    {
        _horizontal = Animator.StringToHash("InputX");
        _vertical = Animator.StringToHash("InputY");
    }

    public void UpdateAnimatorMovementValues(float horizontalMovement, float verticalMovement)
    {
        //Animation Snapping
        float snappedHorizontal;
        float snappedVertical;

        #region Snapped Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion

        #region Snapped Vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion

        _animator.SetFloat(_horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        _animator.SetFloat(_vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
