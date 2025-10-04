using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    [Header("Spawn ranges")]
    private float spawnRangeX = 10f;
    private float spawnZMin = 15f;
    private float spawnZMax = 25f;

    [Header("Wave / difficulty")]
    public int waveCount = 1;
    public float enemyBaseSpeed = 4f;   // base enemy speed
    public float speedPerWave = 0.5f;   // extra speed each new wave

    [Header("Player")]
    public GameObject player;

    public int enemyCount; // for display/debug only

    void Start()
    {
        // kick off the first wave
        SpawnEnemyWave(waveCount);
    }

    void Update()
    {
        // HINT #2: only start a new wave when all ENEMIES are gone
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15f); // spawn near player end

        // Speed for THIS wave, then notify enemies
        float speedThisWave = enemyBaseSpeed + (waveCount - 1) * speedPerWave;
        EnemyX.SetGlobalSpeed(speedThisWave);   // <— enemies read this

        // If no powerups remain, spawn one
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset,
                        powerupPrefab.transform.rotation);
        }

        // HINT #4: spawn exactly 'enemiesToSpawn' enemies (wave 1→1, wave 2→2, …)
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        waveCount++;
        ResetPlayerPosition();
    }

    void ResetPlayerPosition()
    {
        if (player == null) return;

        player.transform.position = new Vector3(0, 1, -7);
        var rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
