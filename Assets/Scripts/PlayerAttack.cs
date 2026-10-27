
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.8f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int attackDamage = 1;

    private void Update()
    {                                                       // DEGISTIRILECEK ATTACK SISTEMÝ
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    private void Attack()
    {                                                                   // ATTACK RANGE ICERÝSÝNDE KALAN DUÞMANI BULMA
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayers
        );

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
