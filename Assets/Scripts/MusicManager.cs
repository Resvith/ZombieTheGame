using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager s_instance = null;

    void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
            DontDestroyOnLoad(gameObject);
            GetComponent<AudioSource>().Play();
        }
        else if (s_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
