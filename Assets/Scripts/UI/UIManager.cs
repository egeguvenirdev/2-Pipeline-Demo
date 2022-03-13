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

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //first touch to play the game
            if (tapToPlayUI.activeSelf)
            {
                if (/*Input.GetMouseButtonDown(0)*/ touch.phase == TouchPhase.Began)
                {
                    tapToPlayUI.SetActive(false);
                    movement.MovementStart();
                    isPaused = false;
                }
            }

            if (restartMenuUI.activeSelf)
            {
                if (/*Input.GetMouseButtonDown(0)*/ touch.phase == TouchPhase.Began)
                {
                    restartMenuUI.SetActive(false);
                    movement.MovementStart();
                    isDead = false;
                    isPaused = false;
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    public void OnDeath()
    {
        restartMenuUI.SetActive(true);
        isDead = true;
        isPaused = true;
    }

    public void HighScore()
    {
        currentScore += 10;
        currentScoreText.text = "Score: " + currentScore.ToString();

        if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.GetInt("HighScore", currentScore);
            highScoreText.text = "High Score: " + currentScore.ToString();
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

    public void UIQuitGame()
    {
        Application.Quit();
    }
}
