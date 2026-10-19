using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject redCoinPrefab;
    [SerializeField] GameObject blueCoinPrefab;

    [SerializeField] Transform player;
    [SerializeField] float interval = 1.5f;
    [SerializeField] int maxCoins = 5;

    // Half-width (X) and half-depth (Z) of the playable room, measured from this object's position
    [SerializeField] Vector2 area = new Vector2(7.5f, 9.5f);
    [SerializeField] float spawnHeight = 0.75f;

    [SerializeField] int blueCoinCount = 50;
    [SerializeField] float blueRadius = 5f;

    void Start() => InvokeRepeating(nameof(SpawnCoin), 1f, interval);

    Vector3 RandomPointInRoom()
    {
        float x = Random.Range(-area.x, area.x);
        float z = Random.Range(-area.y, area.y);
        return transform.position + new Vector3(x, spawnHeight, z);
    }

    public void SpawnCoin()
    {
        if (transform.childCount >= maxCoins) return;

        Vector3 pos = RandomPointInRoom();
        if (Vector3.Distance(pos, player.position) < 2f) return;

        Instantiate(coinPrefab, pos, Quaternion.identity, transform);
    }

    public void SpawnRedCoin()
    {
        Debug.Log("Spawn Red Coin called");

        // Retry until the spot isn't on top of the player, so the spawn can't silently fail
        Vector3 pos = RandomPointInRoom();
        for (int tries = 0; tries < 20; tries++)
        {
            pos = RandomPointInRoom();
            if (Vector3.Distance(pos, player.position) >= 2f) break;
        }

        // No parent: it must not use up one of the normal coin slots
        Instantiate(redCoinPrefab, pos, Quaternion.identity);
    }

    public void SpawnBlueCoin()
    {
        Vector3 c = transform.position;

        // Slide the ring's centre away from the walls so the whole circle fits inside the room
        float limitX = Mathf.Max(0f, area.x - blueRadius);
        float limitZ = Mathf.Max(0f, area.y - blueRadius);
        Vector3 center = new Vector3(
            Mathf.Clamp(player.position.x, c.x - limitX, c.x + limitX),
            spawnHeight,
            Mathf.Clamp(player.position.z, c.z - limitZ, c.z + limitZ));

        for (int i = 0; i < blueCoinCount; i++)
        {
            // Evenly spaced angles around the perimeter
            float angle = i * Mathf.PI * 2f / blueCoinCount;
            Vector3 pos = center + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * blueRadius;

            // Safety net in case the radius is bigger than the room
            pos.x = Mathf.Clamp(pos.x, c.x - area.x, c.x + area.x);
            pos.z = Mathf.Clamp(pos.z, c.z - area.y, c.z + area.y);

            // No parent: these must not count toward maxCoins
            Instantiate(blueCoinPrefab, pos, Quaternion.identity);
        }
    }
}