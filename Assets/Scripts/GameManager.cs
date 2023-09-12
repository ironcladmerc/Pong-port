using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public delegate void GameOver();
    public static event GameOver OnGameOver;

    [SerializeField]
    private int playerScore = 0;

    [SerializeField]
    private int aIScore = 0;

    [SerializeField]
    private int targetScore = 5;

    private bool isGameOver = false;

    [SerializeField]
    private TextMeshProUGUI playerScoreText;

    [SerializeField]
    private TextMeshProUGUI aiScoreText;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private GameObject ballPrefab;

    private AudioSource audioSource;

    private void OnEnable()
    {
        Ball.OnScored += this.OnScored;
    }

    private void OnDisable()
    {
        Ball.OnScored -= this.OnScored;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(SpawnBall());
    }

    private void UpdateScores()
    {
        playerScoreText.text = playerScore.ToString();
        aiScoreText.text = aIScore.ToString();

        if ((playerScore >= targetScore) || (aIScore >= targetScore))
        {
            isGameOver = true;
            OnGameOver();
            gameOverScreen.SetActive(true);
        }
    }

    private void OnScored(string side)
    {
        if (side == "left")
        {
            aIScore++;
        } else
        {
            playerScore++;
        }

        audioSource.Play();

        UpdateScores();

        if (!isGameOver)
        {
            StartCoroutine(SpawnBall());
        }
    }

    private IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(1.0f);

        Instantiate(ballPrefab);

        yield return null;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
