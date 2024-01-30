using System;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    public event Action<Weapon> OnWeaponChanged;

    private string _currentWeaponName;
    private Weapon _currentWeapon;
    private Transform _guns;
    private InputController _inputController;
    private Dictionary<string, Weapon> _weapons = new Dictionary<string, Weapon>();


    void Start()
    {
        _guns = transform;
        _inputController = GameObject.FindGameObjectWithTag("Player").GetComponent<InputController>();
        _inputController.WeaponChange += OnWeaponChange;

        FindWeaponsAndSaveToDictionary();
        SetGunActive("Pistol");

        //PrintWeaponInformations();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _currentWeapon.Shoot();
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            print("key R pressed");
            StartCoroutine(_currentWeapon.Reload());
        }
    }


    private void FindWeaponsAndSaveToDictionary()
    {
        Weapon[] weaponsTab = gameObject.GetComponentsInChildren<Weapon>();
        foreach(Weapon weapon in weaponsTab)
        {
            print(weapon.name);
            _weapons.Add(weapon.name, weapon);
        }
    }


    private void SetGunActive(string gunName)
    {
        foreach (Transform gun in _guns)
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
        if (_weapons.TryGetValue(weaponName, out weapon))
        {
            if (weapon.IsUnlocked && weapon.IsCollected)
            {
                DeactiveWeapon(_currentWeaponName);
                ActivateWeapon(weaponName);
                _currentWeaponName = weaponName;
                _currentWeapon = weapon;
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
        foreach (Transform gun in _guns)
        {
            if (gun.name == name)
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    private void ActivateWeapon(string name)
    {
        foreach (Transform gun in _guns)
        {
            if (gun.name == name)
            {
                gun.gameObject.SetActive(true);
            }
        }
    }

    private void PrintWeaponInformations()
    {
        foreach (var weaponEntry in _weapons)
        {
            Weapon weapon = weaponEntry.Value;
            Debug.Log(weapon.PrintInformation());
        }
    }
}

