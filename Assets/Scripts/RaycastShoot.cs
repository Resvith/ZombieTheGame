using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    private int gunDamage = 1;                              
    private float fireRate = 0.25f;                                       
    private float weaponRange = 50f;                                     
    private Transform gunEnd;                                   
    private Camera fpsCam;                                                
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);     
    private AudioSource gunAudio;                                     
    private LineRenderer laserLine;                                        
    private float nextFire;                    


    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
        SetRaycastColor(Color.gray);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>().OnWeaponChanged += ChangeWeaponParrameters;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>().OnWeaponChange("Pistol");
    }

    void ChangeWeaponParrameters(Weapon weapon)
    {
        gunDamage = weapon.Damage;
        fireRate = weapon.FireRate;
        weaponRange = weapon.Range;
        gunEnd = weapon.GunEnd;
    }

    void SetRaycastColor(Color color)
    {
        Material laserMaterial = laserLine.material;
        laserMaterial.color = color;
        laserLine.material = laserMaterial;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFire)
        {
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
    }


    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
