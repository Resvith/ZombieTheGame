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
    private Transform gunEnd;

    public string Name { get => name; }
    public int BackbackAmmunition { get => backbackAmmunition; set => backbackAmmunition = value; }
    public int MagazineAmmutnition { get => magazineAmmutnition; set => magazineAmmutnition = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }
    public int UnlockingScore { get => unlockingScore; }
    public bool IsCollected { get => isCollected; set => isCollected = value; }
    public Transform GunEnd { get => gunEnd; set => gunEnd = value; }

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
}

public class WeaponController : MonoBehaviour
{
    Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();

    private InputController inputController;

    void Start()
    {
        inputController = GameObject.FindGameObjectWithTag("Player").GetComponent<InputController>();
        inputController.WeaponChange += OnWeaponChange;
        //CreateWeapons();
        //FindGunEnds();

    }

    private void CreateWeapons()
    {
        weapons.Add("Pistol", new Weapon("Pistol", 999, 20, 20, true, 0, true));
        weapons.Add("Shotgun", new Weapon("Shotgun", 50, 8, 8, false, 1000, false));
        weapons.Add("Ak-47", new Weapon("Ak-47", 90, 30, 30, false, 2000, false));
    }

    private void FindGunEnds()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Weapon"))
            {
                print("trying");
                Weapon weapon;
                if (weapons.TryGetValue(child.name, out weapon))
                {
                    Transform gunEnd = child.Find("GunEnd"); // Znajdü child o nazwie 'gunEnd'
                    if (gunEnd != null)
                    {
                        weapon.GunEnd = gunEnd; // Przypisz gunEnd do broni w s≥owniku
                    }
                    else
                    {
                        Debug.LogError("GunEnd nie znaleziony dla broni: " + child.name);
                    }
                }
                else
                {
                    Debug.LogError("Nie znaleziono broni w s≥owniku: " + child.name);
                }
            }
        }
    }

    private void OnWeaponChange(string weaponName)
    {
        Debug.Log("Weapon changed to " + weaponName);
    }
}

