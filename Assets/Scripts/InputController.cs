using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    MovementController movementController;
    private float originalTargetMovingSpeed;
    private float targetSneakingSpeed;
    void Start()
    {
        movementController = GetComponent<MovementController>();
        originalTargetMovingSpeed = movementController.targetMovingSpeed;
        targetSneakingSpeed = originalTargetMovingSpeed / 2;
    }

    void FixedUpdate()
    {
        MovementController();
        JumpController();
        SneakingController();
        ClimbingController();
    }

    private void MovementController()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movementController.isOnLadder)
        {
            targetVelocity = new Vector2(targetVelocity[0], 0);
        }
        movementController.Move(targetVelocity);
    }

    private void JumpController()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            movementController.Jump();
        }
    }

    private void SneakingController()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementController.targetMovingSpeed = targetSneakingSpeed;
        }

        else
        {
            movementController.targetMovingSpeed = originalTargetMovingSpeed;
        }
    }

    private void ClimbingController()
    {
        if (movementController.isOnLadder)
        {
            if (Input.GetKey(KeyCode.W))
            {
                movementController.ClimbUp();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                movementController.ClimbDown();
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                movementController.ClimbStop();
            }
            else
            {
                movementController.ClimbWait();
            }
        }
    }

}
