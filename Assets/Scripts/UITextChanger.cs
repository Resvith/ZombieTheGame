using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UITextChanger : MonoBehaviour
{
    public Text hp;
    public Text totalAmumnition;
    public Text magazineAmmunition;
    public Text score;

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnHealthChanged += ChangeHealthText;
    }

    private void ChangeHealthText(int health)
    {
        hp.text = health.ToString();
    }
}
