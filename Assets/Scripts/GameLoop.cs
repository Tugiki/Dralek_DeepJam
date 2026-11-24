
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float survivalDuration = 120f;

    [Header("Spawn System")]
    [SerializeField] private EnemySpawner enemySpawner;

    private float elapsedTime;
    private bool gameStarted;
    private bool bossPhase;

    private void Start()
    {
        elapsedTime = 0f;
        gameStarted = true;
        bossPhase = false;

        Debug.Log("Harita 1 baþladý!");
    }

    private void Update()
    {
        if (!gameStarted || bossPhase)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= survivalDuration)
        {
            StartBossPhase();
        }
    }

    private void StartBossPhase()
    {
        bossPhase = true;
        gameStarted = false;

        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }

        Debug.Log("Hayatta kalma süresi tamamlandý! Boss aþamasý baþladý!");
    }
}
