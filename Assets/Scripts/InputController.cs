using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    MovementController movementController;
    void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementController.Move(targetVelocity);

        if (Input.GetKey(KeyCode.Space))
        {
            movementController.Jump();

        }
    }

}
