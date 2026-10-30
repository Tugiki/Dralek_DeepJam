
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.8f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackInterval = 1f;
    
    private PlayerController playerController;
    private float nextAttackTime;
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

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayers
        );

        if (hitEnemies.Length > 0 && Time.time >= nextAttackTime)
        {
            Attack(hitEnemies);
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

    private void Attack(Collider2D[] hitEnemies)
    {                                                                   // ATTACK RANGE ICERÝSÝNDE KALAN DUÞMANI BULMA

        Debug.Log("Saldýrý yapýldý! Algýlanan hedef sayýsý: " + hitEnemies.Length);

        foreach (Collider2D enemy in hitEnemies)    // HER DUSMAN ÝCÝN
        {
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
                                                        
            if (health != null)         
            {
                health.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {                                                       // ATTACK RANGE CÝZGÝSÝ
        if (attackPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
