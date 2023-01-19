using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float playTime;
    bool isGameOver = false;

    public GameObject scoreText;
    public GameObject timeText;
    public GameObject gameOverText;
    public PlayerController playerController;
    public GameObject life;

    void OnEnable()
    {
        score = 0;
        playTime = 0;
    }
    void Update()
    {
        if (!isGameOver)
        {
            playTime += Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
        timeText.GetComponent<TextMeshProUGUI>().text = "Time : " + (int)playTime;
        life.GetComponent<TextMeshProUGUI>().text = "Life : " + playerController.Hp;

    }

    public void GetScore()
    {
        score += 100;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score : " + score;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.SetActive(true);

        scoreText.GetComponent<TextMeshProUGUI>().text = "Score : " + score;
    }
}
