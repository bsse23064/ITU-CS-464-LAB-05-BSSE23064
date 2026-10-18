using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
    [SerializeField] int targetScore = 10;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject winPanel;
    [SerializeField] Spawner spawner;
    int score;
    int redCoinSpawnNumber;
    bool redSpawned;
    void Awake() {
        Instance = this;
        redCoinSpawnNumber = Random.Range(1,10);
        Debug.Log(redCoinSpawnNumber);
    } 
    void Start() => UpdateUI();
    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
        if (!redSpawned && score >= redCoinSpawnNumber)
        {
            redSpawned = true;
            spawner.SpawnRedCoin();
        }
        if (score >= targetScore) Win();
    }
    void UpdateUI() => scoreText.text = $"Score: {score} / {targetScore}";
    void Win() { winPanel.SetActive(true); Time.timeScale = 0f; }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
