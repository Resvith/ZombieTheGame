using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    string name;
    int backbackAmmunition;
    int magazineAmmutnition;
    int magazineCapacity;
    private bool isUnlocked;
    private int unlockingScore;
    private bool isCollected;
    private Vector3 gunEnd;

    public string Name { get => name; }
    public int BackbackAmmunition { get => backbackAmmunition; set => backbackAmmunition = value; }
    public int MagazineAmmutnition { get => magazineAmmutnition; set => magazineAmmutnition = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }
    public int UnlockingScore { get => unlockingScore; }
    public bool IsCollected { get => isCollected; set => isCollected = value; }
    public Vector3 GunEnd { get => gunEnd; set => gunEnd = value; }

    public Weapon(string name, int backbackAmmunition, int magazineAmmutnition, int magazineCapacity, bool isUnlocked, int unlockingScore, bool isCollected)
    {
        this.name = name;
        BackbackAmmunition = backbackAmmunition;
        MagazineAmmutnition = magazineAmmutnition;
        this.magazineCapacity = magazineCapacity;
        IsUnlocked = isUnlocked;
        this.unlockingScore = unlockingScore;
        this.IsCollected = isCollected;
    }

    public string PrintInformation()
    {
        return $"Name: {name}, Backpack Ammunition: {backbackAmmunition}, Magazine Ammunition: {magazineAmmutnition}, Magazine Capacity: {magazineCapacity}, Is Unlocked: {isUnlocked}, Unlocking Score: {unlockingScore}, Is Collected: {isCollected}, GunEnd: {GunEnd}";
    }
}

public class WeaponController : MonoBehaviour
{
    Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();
    private string currentWeaponName;
    private Transform guns;

    private InputController inputController;


    void Start()
    {
        guns = transform;
        inputController = GameObject.FindGameObjectWithTag("Player").GetComponent<InputController>();
        inputController.WeaponChange += OnWeaponChange;
        CreateWeapons();
        FindAndSetGunEnds();
        SetGunActive("Pistol");
        //PrintWeaponInformations();
    }

    private void CreateWeapons()
    {
        weapons.Add("Pistol", new Weapon("Pistol", 999, 20, 20, true, 0, true));
        weapons.Add("Shotgun", new Weapon("Shotgun", 50, 8, 8, false, 1000, true));
        weapons.Add("Ak-47", new Weapon("Ak-47", 90, 30, 30, true, 2000, true));
    }

    private void FindAndSetGunEnds()
    {
        foreach (Transform gun in guns)
        {
            name = gun.name;
            Vector3 gunEndPosition = new Vector3();
            if (gun.CompareTag("Weapon"))
            {
                foreach(Transform child in gun)
                {
                    if (child.CompareTag("Waypoint"))
                    {
                        gunEndPosition = child.position;
                    }
                }
            }

            Weapon weapon;
            if (weapons.TryGetValue(name, out weapon))
            {
                if (gunEndPosition != null)
                {
                    weapon.GunEnd = gunEndPosition;
                }
            }
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
    }

    private void OnWeaponChange(string weaponName)
    {
        Weapon weapon;
        if (weapons.TryGetValue(weaponName, out weapon))
        {
            if (weapon.IsUnlocked && weapon.IsCollected)
            {
                DeactiveWeapon(currentWeaponName);
                ActivateWeapon(weaponName);
                currentWeaponName = weaponName;
                print("Changing wepon to: " + weaponName);
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

