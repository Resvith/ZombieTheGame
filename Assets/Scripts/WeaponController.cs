using System;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    public event Action<Weapon> OnWeaponChanged;

    public AudioSource pistolShootEfect;
    public AudioSource shotgunShootEfect;
    public AudioSource akShootEfect;

    Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();
    private string currentWeaponName;
    private Weapon currentWeapon;
    private Transform guns;
    private InputController inputController;


    void Start()
    {
        guns = transform;
        inputController = GameObject.FindGameObjectWithTag("Player").GetComponent<InputController>();
        inputController.WeaponChange += OnWeaponChange;

        FindWeaponsAndSaveToDictionary();
        SetGunActive("Pistol");


        //PrintWeaponInformations();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            currentWeapon.Shoot();
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            print("key R pressed");
            StartCoroutine(currentWeapon.Reload());
        }
    }


    private void FindWeaponsAndSaveToDictionary()
    {
        Weapon[] weaponsTab = gameObject.GetComponentsInChildren<Weapon>();
        foreach(Weapon weapon in weaponsTab)
        {
            print(weapon.name);
            weapons.Add(weapon.name, weapon);
        }
    }


    private void SetGunActive(string gunName)
    {
        foreach (Transform gun in guns)
        {
            if (gun.name == gunName)
            {
                gun.gameObject.SetActive(true);
            }
            else
            {
                gun.gameObject.SetActive(false);
            }
        }
        OnWeaponChange(gunName);
    }

    public void OnWeaponChange(string weaponName)
    {
        Weapon weapon;
        if (weapons.TryGetValue(weaponName, out weapon))
        {
            if (weapon.IsUnlocked && weapon.IsCollected)
            {
                DeactiveWeapon(currentWeaponName);
                ActivateWeapon(weaponName);
                currentWeaponName = weaponName;
                currentWeapon = weapon;
                print("Changing wepon to: " + weaponName);
                OnWeaponChanged?.Invoke(weapon);
            }
            else
            {
                print("Cannot change to: " + weaponName);
            }
        }
    }

    private void DeactiveWeapon(string name)
    {
        foreach (Transform gun in guns)
        {
            if (gun.name == name)
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    private void ActivateWeapon(string name)
    {
        foreach (Transform gun in guns)
        {
            if (gun.name == name)
            {
                gun.gameObject.SetActive(true);
            }
        }
    }

    private void PrintWeaponInformations()
    {
        foreach (var weaponEntry in weapons)
        {
            Weapon weapon = weaponEntry.Value;
            Debug.Log(weapon.PrintInformation());
        }
    }
}

