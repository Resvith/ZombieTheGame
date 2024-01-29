using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public event Action OnShoot;

    private int gunDamage = 1;                              
    private float fireRate = 0.25f;                                       
    private float weaponRange = 50f;    
    private int magazineAmmunition = 20;
    private int backbackAmmunition = 100;
    private int magaineCapacity = 20;
    private float reloadTime;
    private Transform gunEnd;                                   
    private Camera fpsCam;                                                
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);     
    private AudioSource gunAudio;                                     
    private LineRenderer laserLine;                                        
    private float nextFire;
    private Weapon currentWeapon;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
        SetRaycastColor(Color.gray);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>().OnWeaponChanged += ChangeWeaponParrameters;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>().OnWeaponChange("Ak-47");
    }

    void ChangeWeaponParrameters(Weapon weapon)
    {
        gunDamage = weapon.Damage;
        fireRate = weapon.FireRate;
        weaponRange = weapon.Range;
        magazineAmmunition = weapon.MagazineAmmutnition;
        backbackAmmunition = weapon.BackbackAmmunition;
        reloadTime = weapon.ReloadTime;
        magaineCapacity = weapon.MagazineCapacity;
        gunEnd = weapon.GunEnd;

        currentWeapon = weapon;
    }

    void SetRaycastColor(Color color)
    {
        Material laserMaterial = laserLine.material;
        laserMaterial.color = color;
        laserLine.material = laserMaterial;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFire && magazineAmmunition > 0)
        {
            print("magazine" + currentWeapon.MagazineAmmutnition);
            print("backpack" + currentWeapon.BackbackAmmunition);
            OnShoot?.Invoke();
            DecreaseWeaponAmmunition();
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
                Enemy enemy = hit.collider.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(gunDamage);
                }
                
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            print("Reload");
        }
    }

    private IEnumerator Reload()
    {
        if (currentWeapon.BackbackAmmunition > 0)
        {
            int reloadedAmmunition = 0; 
            if (currentWeapon.BackbackAmmunition >= currentWeapon.MagazineCapacity)
            {
                reloadedAmmunition = currentWeapon.MagazineCapacity;
            }
            else
            {
                reloadedAmmunition = currentWeapon.BackbackAmmunition;
            }
            yield return reloadTime;
            currentWeapon.MagazineAmmutnition = reloadedAmmunition;
            currentWeapon.MagazineAmmutnition -= reloadedAmmunition;

        }
        yield return null;
    }

    private void DecreaseWeaponAmmunition()
    {
        currentWeapon.MagazineAmmutnition--;
        magazineAmmunition--;
    }

    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
