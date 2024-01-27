using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private string name;
    private int damage;
    private float range;
    private float fireRate;
    private int backbackAmmunition;
    private int magazineAmmutnition;
    private int magazineCapacity;
    private bool isUnlocked;
    private int unlockingScore;
    private bool isCollected;
    private Vector3 gunEnd;

    public string Name { get => name; }
    public int Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int BackbackAmmunition { get => backbackAmmunition; set => backbackAmmunition = value; }
    public int MagazineAmmutnition { get => magazineAmmutnition; set => magazineAmmutnition = value; }
    public int MagazineCapacity { get => magazineCapacity; set => magazineCapacity = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }
    public int UnlockingScore { get => unlockingScore; }
    public bool IsCollected { get => isCollected; set => isCollected = value; }
    public Vector3 GunEnd { get => gunEnd; set => gunEnd = value; }

    public Weapon(string name, int damage, float range, float fireRate, int backbackAmmunition, int magazineAmmutnition, int magazineCapacity, bool isUnlocked, int unlockingScore, bool isCollected)
    {
        this.name = name;
        Damage = damage;
        Range = range;
        FireRate = fireRate;
        BackbackAmmunition = backbackAmmunition;
        MagazineAmmutnition = magazineAmmutnition;
        MagazineCapacity = magazineCapacity;
        IsUnlocked = isUnlocked;
        this.unlockingScore = unlockingScore;
        IsCollected = isCollected;
    }

    public string PrintInformation()
    {
        return $"Name: {name}," +
            $" Backpack Ammunition: {backbackAmmunition}," +
            $" Magazine Ammunition: {magazineAmmutnition}," +
            $" Magazine Capacity: {MagazineCapacity}," +
            $" Is Unlocked: {isUnlocked}," +
            $" Unlocking Score: {unlockingScore}," +
            $" Is Collected: {isCollected}," +
            $" GunEnd: {GunEnd}";
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
        weapons.Add("Pistol", new Weapon(
                                        name: "Pistol",
                                        damage: 1,
                                        range: 20,
                                        fireRate: 0.15f,
                                        backbackAmmunition: 999,
                                        magazineAmmutnition: 20,
                                        magazineCapacity: 20,
                                        isUnlocked: true,
                                        unlockingScore: 0,
                                        isCollected: true)); 

        weapons.Add("Shotgun", new Weapon(
                                        name: "Shotgun",
                                        damage: 100,
                                        range: 5,
                                        fireRate: 0.3f,
                                        backbackAmmunition: 48,
                                        magazineAmmutnition: 8,
                                        magazineCapacity: 8,
                                        isUnlocked: false,
                                        unlockingScore: 1000,
                                        isCollected: false));

        weapons.Add("Ak-47", new Weapon(
                                        name: "Ak-47",
                                        damage: 10,
                                        range: 50,
                                        fireRate: 0.07f,
                                        backbackAmmunition: 180,
                                        magazineAmmutnition: 30,
                                        magazineCapacity: 30,
                                        isUnlocked: true,
                                        unlockingScore: 2000,
                                        isCollected: true));
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

