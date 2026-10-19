using UnityEngine;

public enum CoinType {Normal,Red,Blue};
public class Coin : MonoBehaviour
{
    [SerializeField] CoinType type = CoinType.Normal;
    [SerializeField] int value = 1;
    [SerializeField] float spinSpeed = 90f;

    void Update() => transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Coin touched by {other.name}");
       if (!other.CompareTag("Player")) return;

        switch (type)
        {
            case CoinType.Normal:
                GameManager.Instance.AddScore(value);
                break;
            case CoinType.Red:
                {
                    Debug.Log("Calling GameManager.OnRedCoinCollected");
                    GameManager.Instance.OnRedCoinCollected();
                    break;
                }
            
            case CoinType.Blue:
                {
                    GameManager.Instance.AddBonus(value);
                    break;
                }

        }

        Destroy(gameObject);
    }
}
