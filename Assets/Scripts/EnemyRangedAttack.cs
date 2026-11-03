using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float attackInterval = 2f;

    private float nextAttackTime;

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        if (target == null || projectilePrefab == null || firePoint == null)
            return;

        if (Time.time >= nextAttackTime)
        {
            Shoot();
            nextAttackTime = Time.time + attackInterval;
        }
    }

    private void Shoot()
    {
        float direction = transform.position.x < 0f ? 1f : -1f;

        Vector3 enemyScale = transform.localScale;
        enemyScale.x = Mathf.Abs(enemyScale.x) * direction;
        transform.localScale = enemyScale;

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );

        EnemyProjectile projectileScript =
            projectile.GetComponent<EnemyProjectile>();

        if (projectileScript != null)
        {
            projectileScript.Launch(Vector2.right * direction);
        }
    }
}