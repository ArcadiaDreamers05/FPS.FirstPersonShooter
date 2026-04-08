using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Nemici")]
    public int totalEnemies = 6;

    [Header("UI Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    private int enemiesAlive;
    private bool gameOver;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        enemiesAlive = totalEnemies;
        winPanel?.SetActive(false);
        losePanel?.SetActive(false);
    }

    public void OnEnemyKilled()
    {
        if (gameOver) return;
        enemiesAlive--;
        if (enemiesAlive <= 0) Win();
    }

    public void OnPlayerDied()
    {
        if (gameOver) return;
        Lose();
    }

    void Win()
    {
        gameOver = true;
        EndGame();
        winPanel?.SetActive(true);
    }

    void Lose()
    {
        gameOver = true;
        EndGame();
        losePanel?.SetActive(true);
    }

    void EndGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}