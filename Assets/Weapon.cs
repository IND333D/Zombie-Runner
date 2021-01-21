using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpcamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleParticle;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject hitmark;
    [SerializeField] Ammo ammocount;
    [SerializeField] float timeBetweenShots = 0.5f;

    bool canshoot = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canshoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canshoot = false;
        if(ammocount.CurrentAmmo() > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammocount.ReduceAmmo();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canshoot = true;
    }

    void PlayMuzzleFlash()
    {
        muzzleParticle.Play();
    }

    void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpcamera.transform.position, fpcamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            CreateHitEffects(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                return;
            }
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    void CreateHitEffects(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        GameObject impactmark = Instantiate(hitmark, hit.point, Quaternion.LookRotation(hit.normal));
        impactmark.transform.SetParent(hit.transform);
        ParticleSystem effect = impact.GetComponentInChildren<ParticleSystem>();
        effect.Play();
        Destroy(impactmark, 8f);
        Destroy(impact,.3f);
    }
}
