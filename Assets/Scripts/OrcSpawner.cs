using UnityEngine;

public class OrcSpawner : MonoBehaviour
{
    public GameObject orcPrefab;
    public int totalOrcs = 8;
    public float spawnInterval = 3f;
    public GameObject portalPiece;

    private int orcsSpawned = 0;
    private int orcsAlive = 0;
    private float spawnTimer = 0f;

    public float minX = -7f;
    public float maxX = 7f;
    public float minY = -4f;
    public float maxY = 4f;

    void Start()
    {
        portalPiece.SetActive(false);
    }

    void Update()
    {
        if (orcsSpawned >= totalOrcs) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnOrc();
        }
    }

    void SpawnOrc()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );

        GameObject orc = Instantiate(orcPrefab, randomPos, Quaternion.identity);
        orc.GetComponent<OrcController>().SetSpawner(this);
        orcsSpawned++;
        orcsAlive++;
    }

    public void OnOrcDied()
    {
        orcsAlive--;
        if (orcsSpawned >= totalOrcs && orcsAlive <= 0)
            portalPiece.SetActive(true);
    }
}