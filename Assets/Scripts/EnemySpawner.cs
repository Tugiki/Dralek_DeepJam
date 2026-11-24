
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject normalEnemyPrefab;
    [SerializeField] private GameObject rangedEnemyPrefab;
    [SerializeField] private GameObject flyingEnemyPrefab;

    [Header("Spawn Groups")]
    [SerializeField] private Transform normalSpawnGroup;
    [SerializeField] private Transform rangedSpawnGroup;
    [SerializeField] private Transform flyingSpawnGroup;

    [Header("Ground")]
    [SerializeField] private Collider2D groundCollider;

    [Header("Spawn Intervals")]
    [SerializeField] private float normalSpawnInterval = 3f;
    [SerializeField] private float rangedSpawnInterval = 7f;
    [SerializeField] private float flyingSpawnInterval = 5f;

    [Header("Maximum Alive Enemies")]
    [SerializeField] private int maxNormalEnemies = 15;
    [SerializeField] private int maxRangedEnemies = 3;
    [SerializeField] private int maxFlyingEnemies = 6;

    [Header("Spawn Timing")]
    [SerializeField] private float flyingStartTime = 20f;
    [SerializeField] private float rangedStartTime = 30f;

    [Header("Spawn Positions")]
    [SerializeField] private float leftLimit = -5.5f;
    [SerializeField] private float rightLimit = 5.5f;
    [SerializeField] private float mapLeftEdge = -6.5f;
    [SerializeField] private float mapRightEdge = 6.5f;
    [SerializeField] private float flyingSpawnY = 0f;
    [SerializeField] private float normalRiseDistance = 3f;

    private float elapsedTime;
    private float nextNormalSpawnTime;
    private float nextRangedSpawnTime;
    private float nextFlyingSpawnTime;

    private bool spawning = true;

    private void Start()
    {
        elapsedTime = 0f;

        nextNormalSpawnTime = 0f;
        nextFlyingSpawnTime = flyingStartTime;
        nextRangedSpawnTime = rangedStartTime;

        Debug.Log("Enemy spawn sistemi baþladý!");
    }

    private void Update()
    {
        if (!spawning)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= nextNormalSpawnTime)
        {
            SpawnNormalEnemy();
            nextNormalSpawnTime += normalSpawnInterval;
        }

        if (elapsedTime >= nextFlyingSpawnTime)
        {
            SpawnFlyingEnemy();
            nextFlyingSpawnTime += flyingSpawnInterval;
        }

        if (elapsedTime >= nextRangedSpawnTime)
        {
            SpawnRangedEnemy();
            nextRangedSpawnTime += rangedSpawnInterval;
        }
    }

    private void SpawnNormalEnemy()
    {
        if (normalEnemyPrefab == null ||
            normalSpawnGroup == null ||
            groundCollider == null)
        {
            return;
        }

        if (normalSpawnGroup.childCount >= maxNormalEnemies)
        {
            return;
        }

        float spawnX = Random.Range(leftLimit, rightLimit);
        float groundY = groundCollider.bounds.max.y;

        GameObject enemy = Instantiate(
            normalEnemyPrefab,
            new Vector3(spawnX, groundY, 0f),
            Quaternion.identity,
            normalSpawnGroup
        );

        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();

        if (enemyCollider != null)
        {
            float spawnY = groundY +
                enemyCollider.bounds.extents.y -
                normalRiseDistance;

            enemy.transform.position = new Vector3(
                spawnX,
                spawnY,
                0f
            );
        }

        Debug.Log("Normal Enemy spawn oldu!");
    }

    private void SpawnRangedEnemy()
    {
        if (rangedEnemyPrefab == null ||
            rangedSpawnGroup == null ||
            groundCollider == null)
        {
            return;
        }

        if (rangedSpawnGroup.childCount >= maxRangedEnemies)
        {
            return;
        }

        float spawnX = Random.value < 0.5f
            ? leftLimit
            : rightLimit;

        float groundY = groundCollider.bounds.max.y;

        GameObject enemy = Instantiate(
            rangedEnemyPrefab,
            new Vector3(spawnX, groundY, 0f),
            Quaternion.identity,
            rangedSpawnGroup
        );

        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();

        if (enemyCollider != null)
        {
            enemy.transform.position = new Vector3(
                spawnX,
                groundY + enemyCollider.bounds.extents.y,
                0f
            );
        }

        Debug.Log("RangedEnemy spawn oldu!");
    }

    private void SpawnFlyingEnemy()
    {
        if (flyingEnemyPrefab == null ||
            flyingSpawnGroup == null)
        {
            return;
        }

        if (flyingSpawnGroup.childCount >= maxFlyingEnemies)
        {
            return;
        }

        float spawnX = Random.value < 0.5f
            ? mapLeftEdge
            : mapRightEdge;

        Instantiate(
            flyingEnemyPrefab,
            new Vector3(spawnX, flyingSpawnY, 0f),
            Quaternion.identity,
            flyingSpawnGroup
        );

        Debug.Log("FlyingEnemy spawn oldu!");
    }

    public void StopSpawning()
    {
        spawning = false;
        Debug.Log("Enemy spawn sistemi durduruldu!");
    }
}
