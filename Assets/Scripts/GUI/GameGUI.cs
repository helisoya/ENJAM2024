using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Represents the Game's GUI
/// </summary>
public class GameGUI : MonoBehaviour
{
    public static GameGUI instance { get; private set; }

    [Header("Players")]
    [SerializeField] private PlayerGUI[] playersGUI;

    [Header("General")]
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private TextMeshProUGUI timer;

    [Header("End Screen")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;

    [Header("Pause Screen")]
    [SerializeField] private GameObject pauseScreen;

    void Awake()
    {
        instance = this;

        for (int i = 0; i < 2; i++)
        {
            SetPlayerCandyCount(i, 0);
            SetPlayerCooldownFill(i, 0);
        }
    }

    /// <summary>
    /// Toggle the pause menu
    /// </summary>
    public void TogglePauseMenu()
    {
        print("Hello");
        pauseScreen.SetActive(!pauseScreen.activeInHierarchy);
        Time.timeScale = pauseScreen.activeInHierarchy ? 0 : 1;
    }

    /// <summary>
    /// Opens the end screen
    /// </summary>
    /// <param name="player1Score">The player 1's score</param>
    /// <param name="player2Score">The player 2's score</param>
    public void OpenEndScreen(int player1Score, int player2Score)
    {
        gameplayScreen.SetActive(false);
        endScreen.SetActive(true);
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

    /// <summary>
    /// Sets the timer's value
    /// </summary>
    /// <param name="remainingSeconds">The remaining timer</param>
    public void SetTimerValue(int remainingSeconds)
    {
        timer.text = remainingSeconds + "s";
    }

    /// <summary>
    /// Sets a player's cooldown fill amount
    /// </summary>
    /// <param name="playerID">The player's ID</param>
    /// <param name="cooldownFill">The player's cooldown fill amount</param>
    public void SetPlayerCooldownFill(int playerID, float cooldownFill)
    {
        playersGUI[playerID].attackCooldownFill.fillAmount = cooldownFill;
    }

    /// <summary>
    /// Sets a player's candy count
    /// </summary>
    /// <param name="playerID">The player's ID</param>
    /// <param name="candyCount">The player's candy count</param>
    public void SetPlayerCandyCount(int playerID, int candyCount)
    {
        playersGUI[playerID].candyCountFill.text = "x" + candyCount;
    }


    public void Click_ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    /// <summary>
    /// Represents a player's GUI
    /// </summary>
    [System.Serializable]
    public class PlayerGUI
    {
        public Image attackCooldownFill;
        public TextMeshProUGUI candyCountFill;
    }
}
