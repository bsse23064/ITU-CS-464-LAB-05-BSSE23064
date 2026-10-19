using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int targetScore = 10;
    [SerializeField] int bonusTargetScore = 50;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text bonusScoreText;   // hidden until the red coin is collected
    [SerializeField] GameObject winPanel;
    [SerializeField] Spawner spawner;

    int score;
    int bonusScore;
    int redCoinSpawnNumber;
    bool redSpawned;
    bool bonusStarted;

    void Awake()
    {
        Instance = this;
        redCoinSpawnNumber = Random.Range(1, 11);   // 1 to 10 inclusive
        Debug.Log($"Red coin will spawn at score {redCoinSpawnNumber}");
    }

    void Start()
    {
        UpdateUI();

        if (bonusScoreText == null)
            Debug.LogError("GameManager: Bonus Score Text is not assigned in the Inspector.");
        else
            bonusScoreText.gameObject.SetActive(false);   // hidden at the start
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();

        if (!redSpawned && score >= redCoinSpawnNumber)
        {
            redSpawned = true;
            spawner.SpawnRedCoin();
        }

        CheckWin();
    }

    public void OnRedCoinCollected()
    {
        if (bonusStarted) return;
        bonusStarted = true;

        UpdateBonusUI();
        if (bonusScoreText != null) bonusScoreText.gameObject.SetActive(true);

        spawner.SpawnBlueCoin();
    }

    public void AddBonus(int amount)
    {
        bonusScore += amount;          // bonus score, not the regular score
        UpdateBonusUI();
        CheckWin();
    }

    void CheckWin()
    {
        if (score >= targetScore && bonusScore >= bonusTargetScore) Win();
    }

    void UpdateUI() => scoreText.text = $"Score: {score} / {targetScore}";

    void UpdateBonusUI()
    {
        if (bonusScoreText == null) return;   // never let a missing label break coin collection
        bonusScoreText.text = $"BonusCoins: {bonusScore}/{bonusTargetScore}";
    }

    void Win()
    {
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}