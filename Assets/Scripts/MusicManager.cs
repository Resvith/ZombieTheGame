using UnityEngine;

public class MusicManager : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
        AudioSource music = GetComponent<AudioSource>();
        music.Play();
    }

}
