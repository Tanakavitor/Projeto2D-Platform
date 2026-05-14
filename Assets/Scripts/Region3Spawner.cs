using UnityEngine;

public class Region3Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject slimePrefab;
    public GameObject orcPrefab;

    [Header("Configuracao")]
    public float spawnInterval = 6f;
    public int slimesPerWave = 3;
    public int orcsPerWave = 2;
    public GameObject portalPiece;

    [Header("Limites do Mapa")]
    public float minX = -7f;
    public float maxX = 7f;
    public float minY = -4f;
    public float maxY = 4f;

    private bool bossAlive = true;
    private float spawnTimer = 0f;
    private bool spawnSlimes = true;

    void Start()
    {
        portalPiece.SetActive(false);
    }

    void Update()
    {
        if (!bossAlive) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        if (spawnSlimes)
        {
            for (int i = 0; i < slimesPerWave; i++)
                SpawnEnemy(slimePrefab);
        }
        else
        {
            for (int i = 0; i < orcsPerWave; i++)
                SpawnEnemy(orcPrefab);
        }
        spawnSlimes = !spawnSlimes;
    }

    void SpawnEnemy(GameObject prefab)
    {
        Vector2 randomPos = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );
        Instantiate(prefab, randomPos, Quaternion.identity);
    }

    public void OnBossDied()
    {
        bossAlive = false;
        portalPiece.SetActive(true);
    }
}