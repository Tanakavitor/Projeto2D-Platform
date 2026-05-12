using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject slimePrefab;
    public int totalSlimes = 10;
    public float spawnInterval = 2f;
    public GameObject portalPiece;

    private int slimesSpawned = 0;
    private int slimesAlive = 0;
    private float spawnTimer = 0f;

    // Define os limites do mapa para spawn aleatorio
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4f;
    public float maxY = 4f;

    void Start()
    {
        portalPiece.SetActive(false);
    }

    void Update()
    {
        if (slimesSpawned >= totalSlimes) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnSlime();
        }
    }

    void SpawnSlime()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );

        GameObject slime = Instantiate(slimePrefab, randomPos, Quaternion.identity);
        slime.GetComponent<SlimeController>().SetSpawner(this);
        slimesSpawned++;
        slimesAlive++;
    }

    public void OnSlimeDied()
    {
        slimesAlive--;
        if (slimesSpawned >= totalSlimes && slimesAlive <= 0)
            portalPiece.SetActive(true);
    }
}