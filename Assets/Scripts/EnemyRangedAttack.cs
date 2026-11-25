
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float minAttackInterval = 1.5f;
    [SerializeField] private float maxAttackInterval = 4f;

    private float nextAttackTime;

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        if (projectilePrefab == null || firePoint == null)
            return;

        if (Time.time >= nextAttackTime)
        {
            Shoot();

            float randomInterval = Random.Range(
                minAttackInterval,
                maxAttackInterval
            );

            nextAttackTime = Time.time + randomInterval;
        }
    }

    private void Shoot()
    {
        float screenMiddle = Camera.main.transform.position.x;
        float direction = transform.position.x < screenMiddle ? 1f : -1f;

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
