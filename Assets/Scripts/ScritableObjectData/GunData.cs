using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "Scriptable Weapons/Gun")]
public class GunData : ScriptableObject
{
    public string gunName;

    public float damage;
    public float bulletsPerTap;
    public float fireRate;
    public float timeBetweenFiring;
    public float range;

    public float reloadTime;
    public int magazineSize;
}
