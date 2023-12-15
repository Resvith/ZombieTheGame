using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderColision : MonoBehaviour
{
    MovementController movementController;
    private void Start()
    {
        //movementController = GetComponent<MovementController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
/*        if (collision != null && collision.collider.name == "Player")
        {
            movementController.isOnLadder = true;
            Debug.Log("Player COllision");
        }*/
    }

    private void OnCollisionExit(Collision collision)
    {
/*        if (collision != null && collision.collider.name == "Player")
        {
            movementController.isOnLadder = false;
            Debug.Log("Player Exit");
        }*/
    }
}
