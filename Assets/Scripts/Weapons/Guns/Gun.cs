using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class Gun : MonoBehaviour
{
    public enum GunState
    {
        Idle,
        Firing,
        Reloading
    }

    [Header("Reference")]
    public InputReader input;
    public GunData gunData;
    public Camera playerCam;

    [Header("GunInfo")]
    public GunState currentState;
    public float bulletsFired;
    public float currentAmmo;

    
    public RaycastHit rayHit;

    private void Awake()
    {
        input = FindAnyObjectByType<InputReader>();
        currentAmmo = gunData.magazineSize;

        ChangeState(GunState.Idle);
    }

    private void Update()
    {
        Input();

        Debug.DrawLine(playerCam.transform.position,playerCam.transform.position + playerCam.transform.forward * gunData.range, Color.red);
    }

    private void Input()
    {
        if (input.Reload && currentAmmo < gunData.magazineSize)
        {
            TryReload();
            return;
        }

        if(input.Fire)
        {
            TryShoot();
        }
    }

    public void TryShoot()
    {
       if(currentState != GunState.Idle) return;
       if(currentAmmo <= 0) return;

       StartCoroutine(FireRoutine());
    }

    public void TryReload()
    {
        if(currentState == GunState.Reloading) return;
        if(currentAmmo >= gunData.magazineSize) return;

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator FireRoutine()
    {
        ChangeState(GunState.Firing);

        bulletsFired = gunData.bulletsPerTap;

        while (bulletsFired > 0 && currentAmmo > 0)
        {
            Shoot();

            bulletsFired--;

            yield return new WaitForSeconds(gunData.fireRate);
        }

        yield return new WaitForSeconds(gunData.timeBetweenFiring);
        ChangeState(GunState.Idle);
    }

    private void Shoot()
    {
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out rayHit, gunData.range))
        {
            rayHit.collider.TryGetComponent<IDamageable>(out var damageable);
            damageable?.TakeDamage(gunData.damage);
        }

        currentAmmo--;
    }

    private IEnumerator ReloadRoutine()
    {
        ChangeState(GunState.Reloading);

        yield return new WaitForSeconds(gunData.reloadTime);
        currentAmmo = gunData.magazineSize;

        ChangeState(GunState.Idle);
    }

    private void ChangeState(GunState newState)
    {
        currentState = newState;
    }
}

