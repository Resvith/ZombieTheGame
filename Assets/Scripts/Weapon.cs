using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform gunEnd;
    public event Action<Weapon> OnShoot;
    public event Action<Weapon> Reloaded;

    private Camera fpsCam;
    private LineRenderer laserLine;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;
    private string weaponName;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    [SerializeField] private int backbackAmmunition;
    [SerializeField] private int magazineAmmutnition;
    [SerializeField] private int magazineCapacity;
    [SerializeField] private float reloadTime;
    private bool reloading = false;
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
    public float NextFire { get => nextFire; set => nextFire = value; }


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

    private void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunShot = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
        SetRaycastColor(Color.gray);
    }

    void SetRaycastColor(Color color)
    {
        Material laserMaterial = laserLine.material;
        laserMaterial.color = color;
        laserLine.material = laserMaterial;
    }

    public void Shoot()
    {
        magazineAmmutnition--;
        OnShoot?.Invoke(this);
        NextFire = Time.time + fireRate;
        StartCoroutine(ShotEffect());
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, range))
        {
            laserLine.SetPosition(1, hit.point);
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * range));
        }
    }


   
    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

    public IEnumerator Reload()
    {
        if (!reloading && backbackAmmunition > 0 && magazineAmmutnition < magazineCapacity)
        {
            reloading = true;
            int neededAmmunition = magazineCapacity - magazineAmmutnition;
            int reloadedAmmunition = Math.Min(neededAmmunition, backbackAmmunition);

            yield return new WaitForSeconds(reloadTime);

            backbackAmmunition -= reloadedAmmunition;
            magazineAmmutnition += reloadedAmmunition;

            Reloaded?.Invoke(this);

            reloading = false;
        }
        else
        {
            yield break;
        }
    }

}
