using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<MovementController>().isOnLadder = true;
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
