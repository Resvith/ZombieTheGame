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
    public Text backpackAmmunition;
    public Text magazineAmmunition;
    public Text score;

    private Player player;
    private WeaponController weaponController;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnHealthChanged += ChangeHealthText;

        weaponController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>();
        weaponController.OnWeaponChanged += OnWeaponChange;
    }

    private void ChangeHealthText(int health)
    {
        hp.text = health.ToString();
    }

    private void OnWeaponChange(Weapon weapon)
    {
        magazineAmmunition.text = weapon.MagazineAmmutnition.ToString();
        backpackAmmunition.text = weapon.BackbackAmmunition.ToString();
    }

    private void OnShoot()
    {
        int magazineAmmunitionInt = int.Parse(magazineAmmunition.text);
        magazineAmmunitionInt--;
        magazineAmmunition.text = magazineAmmunitionInt.ToString();
    }

    private void OnReload()
    {

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
