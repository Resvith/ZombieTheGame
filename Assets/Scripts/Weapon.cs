using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform gunEnd;

    private string weaponName;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    [SerializeField] private int backbackAmmunition;
    [SerializeField] private int magazineAmmutnition;
    [SerializeField] private int magazineCapacity;
    [SerializeField] private float reloadTime;
    [SerializeField] private bool isUnlocked;
    [SerializeField] private int unlockingScore;
    [SerializeField] private bool isCollected;
    private AudioSource gunShot;

    public string WeaponName { get => weaponName; }
    public int Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int BackbackAmmunition { get => backbackAmmunition; set => backbackAmmunition = value; }
    public int MagazineAmmutnition { get => magazineAmmutnition; set => magazineAmmutnition = value; }
    public int MagazineCapacity { get => magazineCapacity; set => magazineCapacity = value; }
    public float ReloadTime { get => reloadTime; set => reloadTime = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }
    public int UnlockingScore { get => unlockingScore; }
    public bool IsCollected { get => isCollected; set => isCollected = value; }
    public Transform GunEnd { get => gunEnd; set => gunEnd = value; }
    public AudioSource GunShot { get => gunShot; set => gunShot = value; }

    public Weapon(string weaponName, int damage, float range, float fireRate, int backbackAmmunition, int magazineAmmutnition, int magazineCapacity, float reloadTime, bool isUnlocked, int unlockingScore, bool isCollected, AudioSource gunShot)
    {
        this.weaponName = weaponName;
        Damage = damage;
        Range = range;
        FireRate = fireRate;
        BackbackAmmunition = backbackAmmunition;
        MagazineAmmutnition = magazineAmmutnition;
        MagazineCapacity = magazineCapacity;
        ReloadTime = reloadTime;
        IsUnlocked = isUnlocked;
        this.unlockingScore = unlockingScore;
        IsCollected = isCollected;
        GunShot = gunShot;
    }

    public string PrintInformation()
    {
        return $"Name: {weaponName}," +
            $" Backpack Ammunition: {backbackAmmunition}," +
            $" Magazine Ammunition: {magazineAmmutnition}," +
            $" Magazine Capacity: {MagazineCapacity}," +
            $" Is Unlocked: {isUnlocked}," +
            $" Unlocking Score: {unlockingScore}," +
            $" Is Collected: {isCollected}," +
            $" GunEnd: {GunEnd}";
    }
}
