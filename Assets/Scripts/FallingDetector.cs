using UnityEngine;

public class FallingDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (player != null)
                player.TakeDamage(999);
        }

        else
            Destroy(other.gameObject);
    }
}
