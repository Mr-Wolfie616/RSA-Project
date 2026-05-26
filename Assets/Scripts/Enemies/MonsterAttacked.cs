using UnityEngine;

public class monsterdeath : MonoBehaviour, IDamageable
{

    [SerializeField] private float Mhealth = 25f;

    public void TakeDamage(float damage)
    {
        Mhealth -= damage;
        
        Debug.Log(gameObject + "took" + damage + "damage.Health" + Mhealth);

        if (Mhealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + "died.");
        Destroy(gameObject);
    }
}
