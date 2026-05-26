using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public InputReader input;
    public GunData gunData;
    public float bulletsFired;
    public float currentAmmo;
    public bool shooting;
    public bool readyToFire;
    public bool reloading;

    public Camera playerCam;
    public RaycastHit rayHit;

    private void Awake()
    {
        input = FindAnyObjectByType<InputReader>();
        currentAmmo = gunData.magazineSize;
        readyToFire = true;
    }

    private void Update()
    {
        InputHandler();
        Debug.DrawLine(playerCam.transform.position,playerCam.transform.position + playerCam.transform.forward * gunData.range, Color.red);
    }

    private void InputHandler()
    {
        shooting = input.Fire;

        if (input.Reload && currentAmmo < gunData.magazineSize && !reloading)
        Reload();

        if(readyToFire && shooting && !reloading && currentAmmo > 0)
        {
            bulletsFired = gunData.bulletsPerTap;
            Shoot();
        }
    }

    public void Shoot()
    {
       readyToFire = false; 

       if(Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out rayHit, gunData.range))
        {
            Debug.Log(rayHit.collider.name);

            rayHit.collider.TryGetComponent<IDamageable>(out var damageable);
            damageable?.TakeDamage(gunData.damage);
        }

        currentAmmo --;
        bulletsFired --;

        Invoke ("ResetFire", gunData.timeBetweenFiring);

        if (bulletsFired > 0 && currentAmmo > 0)
        Invoke ("Shoot", gunData.fireRate );
    }

    void ResetFire()
    {
        readyToFire = true;
    }

    public void Reload()
    {
        reloading = true;
        Invoke ("ReloadFinish", gunData.reloadTime);
        Debug.Log("Reload");
    }

    void ReloadFinish()
    {
        currentAmmo = gunData.magazineSize;
        reloading = false;
    }
}

