
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    [Header("Normal Saldýrý")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.6f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackInterval = 0.1f;

    [Header("Güçlü Saldýrý")]
    [SerializeField] private float strongAttackCooldown = 30f;
    [SerializeField] private float strongAttackRangeMultiplier = 3f;
    [SerializeField] private float strongAttackDamageMultiplier = 3f;

    private PlayerController playerController;
    private float nextAttackTime;
    private float nextStrongAttackTime;
    private float attackPointStartX;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        if (attackPoint != null)
        {
            attackPointStartX = Mathf.Abs(attackPoint.localPosition.x);
        }
    }
    private void Update()
    {                                                       // DEGISTIRILECEK ATTACK SISTEMÝ
        if (Time.timeScale == 0f)
        {
            return;
        }

        UpdateAttackDirection();

        if (attackPoint == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) &&
            Time.time >= nextStrongAttackTime)
        {
            float strongRange =
                attackRange * strongAttackRangeMultiplier;

            int strongDamage =
                Mathf.RoundToInt(attackDamage * strongAttackDamageMultiplier);

            Collider2D[] strongHitEnemies = Physics2D.OverlapCircleAll(
                attackPoint.position,
                strongRange,
                enemyLayers
            );

            if (strongHitEnemies.Length > 0)
            {
                Attack(strongHitEnemies, strongDamage);
                Debug.Log("Güçlü saldýrý yapýldý!");
            }

            nextStrongAttackTime = Time.time + strongAttackCooldown;

            return;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayers
        );

        if (hitEnemies.Length > 0 && Time.time >= nextAttackTime)
        {
            Attack(hitEnemies, attackDamage);
            nextAttackTime = Time.time + attackInterval;
        }
    }

    private void UpdateAttackDirection()
    {
        if (playerController == null || attackPoint == null)
        {
            return;
        }

        float direction = playerController.FacingDirection;

        Vector3 position = attackPoint.localPosition;
        position.x = attackPointStartX * direction;
        attackPoint.localPosition = position;
    }

    private void Attack(Collider2D[] hitEnemies, int damage)
    {
        Debug.Log("Saldýrý yapýldý! Algýlanan hedef sayýsý: " + hitEnemies.Length);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {                                                       // ATTACK RANGE CÝZGÝSÝ
        if (attackPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange * 3f);
        }
    }
}
