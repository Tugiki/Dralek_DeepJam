
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackInterval = 1f;

    private PlayerHealth playerHealth;
    private float nextAttackTime;

    private void Update()
    {
        if (target == null)
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
}
