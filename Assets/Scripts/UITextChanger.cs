using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
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

    public void IncreaseScore(int score)
    {
        string scoreText = this.score.text;
        int scoreInt;
        try
        {
            scoreInt = int.Parse(scoreText.Substring(6));
        }
        catch (ArgumentNullException)
        {
            scoreInt = 10;
        }

        this.score.text = "Score: " + (score + scoreInt);
    }
}
