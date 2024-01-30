using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<string> WeaponChange;

    private MovementController _movementController;
    private float _originalTargetMovingSpeed;
    private float _targetSneakingSpeed;


    void Start()
    {
        _movementController = GetComponent<MovementController>();
        _originalTargetMovingSpeed = _movementController.targetMovingSpeed;
        _targetSneakingSpeed = _originalTargetMovingSpeed / 2;
    }

    void FixedUpdate()
    {
        MovementController();
        JumpController();
        SneakingController();
        ClimbingController();
        WeaponController();
    }

    private void MovementController()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (_movementController.isOnLadder)
        {
            targetVelocity = new Vector2(targetVelocity[0], 0);
        }
        _movementController.Move(targetVelocity);
    }

    private void JumpController()
    {
        if (Input.GetKey(KeyCode.Space))
            _movementController.Jump();
    }

    private void SneakingController()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            _movementController.targetMovingSpeed = _targetSneakingSpeed;
        else
            _movementController.targetMovingSpeed = _originalTargetMovingSpeed;
    }

    private void ClimbingController()
    {
        if (_movementController.isOnLadder)
        {
            if (Input.GetKey(KeyCode.W))
            {
                _movementController.ClimbUp();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (_movementController.isGrounded)
                {
                    _movementController.ClimbStop();
                    Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                    _movementController.Move(targetVelocity);
                }
                else
                {
                    _movementController.ClimbDown();
                }
                _movementController.ClimbDown();
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                _movementController.ClimbStop();
            }
            else
            {
                _movementController.ClimbWait();
            }
        }
    }

    private void WeaponController()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            WeaponChange?.Invoke("Pistol");
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            WeaponChange?.Invoke("Shotgun");
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            WeaponChange?.Invoke("Ak-47");
        }
    }
}
