using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    int score;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ShowWin()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }
    public void Win()
    {
        Invoke(nameof(ShowWin), 1.75f);
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}