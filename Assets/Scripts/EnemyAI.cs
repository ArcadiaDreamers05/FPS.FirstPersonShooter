using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Combattimento")]
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public float attackRate = 1f;

    [Header("Fuga")]
    public float fleeHealthThreshold = 25f;
    public float fleeDistance = 12f;
    public float tooCloseDistance = 3f;

    private NavMeshAgent agent;
    private Health health;
    private Transform player;
    private float nextAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null || health == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        bool hpBassa = health.currentHealth <= fleeHealthThreshold;
        bool troppoVicino = dist <= tooCloseDistance;

        if (hpBassa || troppoVicino)
        {
            Flee();
        }
        else
        {
            Chase(dist);
        }
    }

    void Chase(float dist)
    {
        agent.SetDestination(player.position);

        if (dist <= attackRange && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackRate;
            player.GetComponent<Health>()?.TakeDamage(attackDamage);
        }
    }

    void Flee()
    {
        Vector3 dir = (transform.position - player.position).normalized;
        Vector3 target = transform.position + dir * fleeDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(target, out hit, fleeDistance, NavMesh.AllAreas))
            agent.SetDestination(hit.position);
    }
}