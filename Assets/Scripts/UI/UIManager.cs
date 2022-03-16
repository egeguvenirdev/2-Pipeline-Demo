using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle vibrationToggle;
    private int currentScore;
    public bool isPaused;
    public bool isDead;

    private void Awake()
    {
        UIM = this;
        soundToggle.GetComponent<Toggle>().isOn = true;
        if (PlayerPrefs.GetInt("vibrationOnOff") == 0)
        {
            vibrationToggle.GetComponent<Toggle>().isOn = false;
        }

        if (PlayerPrefs.GetInt("soundOnOff") == 0)
        {
            soundToggle.GetComponent<Toggle>().isOn = false;
        }
    }

    private void Start()
    {
        isPaused = true;
        isDead = false;
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
            vibrationToggle.GetComponent<Toggle>().isOn = true;
            PlayerPrefs.SetInt("vibrationOnOff", 1);
        }
        else
        {
            vibrationToggle.GetComponent<Toggle>().isOn = false;
            PlayerPrefs.SetInt("vibrationOnOff", 0);
        }
    }

    public void UISoundToggle(bool checkOnOff)
    {
        if (checkOnOff)
        {
            soundToggle.GetComponent<Toggle>().isOn = true;
            PlayerPrefs.SetInt("soundOnOff", 1);
        }
        else
        {
            soundToggle.GetComponent<Toggle>().isOn = false;
            PlayerPrefs.SetInt("soundOnOff", 0);
        }
    }

    public void UIQuitGame()
    {
        Application.Quit();
    }
}
