
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRange = 0.6f;
    [SerializeField] private float attackInterval = 1f;

    private PlayerHealth playerHealth;
    private float nextAttackTime;

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        if (Time.timeScale == 0f)
        {
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            target.position
        );

        if (distance <= attackRange && Time.time >= nextAttackTime)
        {
            if (playerHealth == null)
            {
                playerHealth = target.GetComponent<PlayerHealth>();
            }

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                nextAttackTime = Time.time + attackInterval;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
