using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<MovementController>().isOnLadder = true;
            print("On ladder");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<MovementController>().isOnLadder = false;
            print("Off ladder");
        }
    }
}
