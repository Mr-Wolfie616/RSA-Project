using UnityEngine;
using UnityEngine.AI;

public class MonsterDeath : MonoBehaviour, IDamageable
{

    [SerializeField] private float Mhealth = 25f;

    private NavMeshAgent agent;
    private Animator anim;
    public bool IsDead;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if(IsDead) return;

        Mhealth -= damage;
        
        Debug.Log(gameObject + "took" + damage + "damage.Health" + Mhealth);

        if (Mhealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        IsDead = true;

        agent.isStopped = true;
        agent.ResetPath();
        agent.velocity = Vector3.zero;

        anim.SetFloat("Speed", 0f);
        anim.SetBool("IsDead", true);
    }
}
