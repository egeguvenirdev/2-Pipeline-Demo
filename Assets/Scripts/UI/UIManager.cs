using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject tapToPlayUI;
    [SerializeField] private GameObject restartMenuUI;
    [SerializeField] private Movement movement; //movement script

    //pause button ui uthilities
    [SerializeField] private GameObject pausedText;
    public bool isPaused = true;
    public bool isDead = false;

    private void Update()
    {
        //first touch to play the game
        if (tapToPlayUI.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                tapToPlayUI.SetActive(false);
                movement.MovementStart();
                isPaused = false;
            }
        }

        if (restartMenuUI.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                restartMenuUI.SetActive(false);
                movement.MovementStart();
                isDead = false;
                isPaused = false;
                SceneManager.LoadScene(0);
            }
        }
    }

    public void OnDeath()
    {
        restartMenuUI.SetActive(true);
        isDead = true;
        isPaused = true;
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
