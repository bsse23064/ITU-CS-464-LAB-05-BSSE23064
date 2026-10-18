using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    
    [SerializeField] GameObject redCoinPrefab;

    [SerializeField] GameObject blueCoinPrefab;
    
    [SerializeField] int redSpawnNumber;
    [SerializeField] Transform player;
    [SerializeField] float interval = 1.5f;
    [SerializeField] int maxCoins = 5;
    [SerializeField] Vector2 area = new Vector2(7.5f, 9.5f);

    void Start() => InvokeRepeating(nameof(SpawnCoin), 1f, interval);

    public void SpawnCoin()
    {
        if (transform.childCount >= maxCoins) return;
        float x = Random.Range(-area.x, area.x);
        float z = Random.Range(-area.y, area.y);
        Vector3 pos = new Vector3(x, 0.75f, z);
        if (Vector3.Distance(pos, player.position) < 2f) return;
        Instantiate(coinPrefab, pos, Quaternion.identity, transform);
    }
    public void SpawnRedCoin()
    {
        Debug.Log("Spawn Red Coin called");
        if (transform.childCount >= maxCoins) return;
        float x = Random.Range(-area.x, area.x);
        float z = Random.Range(-area.y, area.y);
        Vector3 pos = new Vector3(x, 0.75f, z);
        if (Vector3.Distance(pos, player.position) < 2f) return;
        Instantiate(redCoinPrefab, pos, Quaternion.identity, transform);
    }

    public void SpawnBlueCoin()
    {
        if (transform.childCount >= maxCoins) return;
        float x = Random.Range(-area.x, area.x);
        float z = Random.Range(-area.y, area.y);
        Vector3 pos = new Vector3(x, 0.75f, z);
        if (Vector3.Distance(pos, player.position) < 2f) return;
        Instantiate(blueCoinPrefab, pos, Quaternion.identity, transform);
    }
}
