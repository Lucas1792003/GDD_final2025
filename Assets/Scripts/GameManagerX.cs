using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timeText;      // <- create a TMP text in Canvas and assign here (for the timer)
    public GameObject titleScreen;
    public Button restartButton;

    [Header("Spawning")]
    public List<GameObject> targetPrefabs;
    public float baseSpawnRate = 1.5f;    // <- tune in Inspector
    private float spawnRate;

    [Header("State")]
    public bool isGameActive;
    private int score;
    public float timeRemaining = 60f;     // <- countdown (optional)

    // board layout
    private float spaceBetweenSquares = 2.5f;
    private float minValueX = -3.75f;
    private float minValueY = -3.75f;

    // Start the game, remove title, reset score, and adjust spawnRate based on difficulty button clicked
    // difficulty: 1 = Easy, 2 = Medium, 3 = Hard
    public void StartGame(int difficulty)
    {
        spawnRate = baseSpawnRate / Mathf.Max(1, difficulty);
        isGameActive = true;

        score = 0;
        UpdateScore(0);

        timeRemaining = 60f;                          // reset timer
        if (timeText) timeText.text = "Time: 60";

        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);    // hide until game over
        titleScreen.SetActive(false);

        StartCoroutine(SpawnTarget());
    }

    void Update()
    {
        if (!isGameActive) return;

        // 60-second countdown (whole numbers)
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0f) timeRemaining = 0f;
        if (timeText) timeText.text = $"Time: {Mathf.CeilToInt(timeRemaining)}";
        if (Mathf.Approximately(timeRemaining, 0f))
        {
            GameOver();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            if (!isGameActive) break;

            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
        }
    }

    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);
        return new Vector3(spawnPosX, spawnPosY, 0);
    }

    int RandomSquareIndex() => Random.Range(0, 4);

    // Always show "Score: <value>"
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: {score}";
    }

    public void GameOver()
    {
        if (!isGameActive) return;  // prevent double-call
        isGameActive = false;

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);     // <- enable (you had this set to false)
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
