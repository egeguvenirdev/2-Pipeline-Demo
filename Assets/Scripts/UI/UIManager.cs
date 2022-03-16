using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject tapToPlayUI;
    [SerializeField] private GameObject restartMenuUI;
    [SerializeField] private Movement movement; //movement script
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text currentScoreText;
    public static UIManager UIM;

    //pause button ui uthilities
    [SerializeField] private GameObject pausedText;
    private int currentScore;
    public bool isPaused;
    public bool isDead;
    public int vibrationOnOff;
    public int soundOnOff;

    private void Awake()
    {
        UIM = this;

    }
    private void Start()
    {
        isPaused = true;
        isDead = false;
        //highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        currentScore = 0;
    }

    public void OnDeath()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        restartMenuUI.SetActive(true);
        isDead = true;
        isPaused = true;

        IsHighScore();
    }

    public void CurrentScore()
    {
        currentScore += 10;
        currentScoreText.text = "Score: " + currentScore.ToString();

    }

    private void IsHighScore()
    {
        if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }

    public void PlayResButton()
    {
        if (tapToPlayUI.activeSelf)
        {
            tapToPlayUI.SetActive(false);
            movement.MovementStart();
            isPaused = false;
        }


        if (restartMenuUI.activeSelf)
        {
            restartMenuUI.SetActive(false);
            movement.MovementStart();
            isDead = false;
            isPaused = false;
            SceneManager.LoadScene(0);
        }
    }

    public void UIPauseButton()
    {
        if (!isPaused) //if the game not stopped
        {
            movement.MovementStop();
            pausedText.SetActive(true);
            tapToPlayUI.SetActive(true);
            isPaused = true;
        }
    }

    public void UIVibrationToggle(bool checkOnOff)
    {
        if (checkOnOff)
        {
            PlayerPrefs.SetInt("vibrationOnOff", 1);
        }
        else
        {
            PlayerPrefs.SetInt("vibrationOnOff", 0);
        }
    }

    public void UISoundToggle(bool checkOnOff)
    {
        if (checkOnOff)
        {
            PlayerPrefs.SetInt("soundOnOff", 1);
        }
        else
        {
            PlayerPrefs.SetInt("soundOnOff", 0);
        }
    }

    public void UIQuitGame()
    {
        Application.Quit();
    }
}
