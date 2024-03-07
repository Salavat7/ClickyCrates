using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private List<GameObject> _targets;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private bool _paused;
    private int _score;
    private int _lives;
    private float _spawnRate = 1.0f;
    public bool IsGameActive;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarger()
    {
        while(IsGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, _targets.Count);
            Instantiate(_targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        if (_score < 0)
            _score = 0;
        _scoreText.text = "Score: " + _score;
    }

    public void UpdateLives(int livesToAdd)
    {
        _lives += livesToAdd;
        _livesText.text = "Lives: " + _lives;
        if (_lives <= 0)
            GameOver();
    }

    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
        IsGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        IsGameActive = true;
        _score = 0;
        _lives = 3;
        _spawnRate /= difficulty;
        UpdateScore(0);
        UpdateLives(0);
        StartCoroutine(SpawnTarger());
        _titleScreen.gameObject.SetActive(false);
    }

    private void ChangePaused()
    {
        if (!_paused)
        {
            _paused = true;
            _pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _paused = false;
            _pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
