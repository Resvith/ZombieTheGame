using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public event Action EnemyOnLadder;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<MovementController>().isOnLadder = true;
        }

        else if (collision.tag == "Enemy")
        {
            EnemyOnLadder?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<MovementController>().isOnLadder = false;
        }
    }
}
