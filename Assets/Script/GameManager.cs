using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PipeSpawner spawner;
    public GameObject gameOverUI; // assign a UI GameObject to show on death
    public float restartDelay = 1.2f;

    bool isGameOver = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        if (gameOverUI != null) gameOverUI.SetActive(false);
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Stop spawning
        if (spawner != null) spawner.Stop();

        // Show game over UI
        if (gameOverUI != null) gameOverUI.SetActive(true);

        // Optionally, stop moving pipes by setting their speed to 0.
        // Simple approach: find all Pipe scripts and disable them.
        var pipes = FindObjectsOfType<Pipe>();
        foreach (var p in pipes) p.enabled = false;

        // Stop further input on player optionally

        // Restart after a short delay
        Invoke(nameof(RestartScene), restartDelay);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
