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

    [Header("Ready Up")]
    [SerializeField] private GameObject readyUpScreen;
    [SerializeField] private PlayerReadyUp[] playerReadyUps;

    [Header("Players")]
    [SerializeField] private PlayerGUI prefabPlayerGUI;
    [SerializeField] private Transform[] playersGUIRoots;
    private List<PlayerGUI> playersGUI;

    [Header("General")]
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private TextMeshProUGUI timer;

    [Header("End Screen")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private PlayerScore[] scores;

    [Header("Pause Screen")]
    [SerializeField] private GameObject pauseScreen;

    void Awake()
    {
        instance = this;
        playersGUI = new List<PlayerGUI>();
        OpenReadyUpScreen();
    }

    /// <summary>
    /// Toggle the pause menu
    /// </summary>
    public void TogglePauseMenu()
    {
        pauseScreen.SetActive(!pauseScreen.activeInHierarchy);
        Time.timeScale = pauseScreen.activeInHierarchy ? 0 : 1;
    }

    /// <summary>
    /// Opens the end screen
    /// </summary>
    /// <param name="players">The players</param>
    public void OpenEndScreen(List<Player> players)
    {
        gameplayScreen.SetActive(false);
        endScreen.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            if (i < players.Count)
            {
                scores[i].SetScore(players[i].GetScore());
            }
            else
            {
                scores[i].gameObject.SetActive(false);
            }
        }

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
    /// Opens the ready up screen
    /// </summary>
    public void OpenReadyUpScreen()
    {
        readyUpScreen.SetActive(true);
        gameplayScreen.SetActive(false);

        foreach (PlayerReadyUp readyUp in playerReadyUps)
        {
            readyUp.gameObject.SetActive(false);
            readyUp.SetReadyUpCheckActive(false);
        }
    }

    /// <summary>
    /// Opens the gameplay screen
    /// </summary>
    public void OpenGamePlayScreen()
    {
        readyUpScreen.SetActive(false);
        gameplayScreen.SetActive(true);
    }

    /// <summary>
    /// Ready ups a player
    /// </summary>
    /// <param name="ID">The player's ID</param>
    public void ReadyUpPlayer(int ID)
    {
        playerReadyUps[ID].SetReadyUpCheckActive(true);
    }

    /// <summary>
    /// Adds a new player's GUI
    /// </summary>
    /// <param name="ID">The player's ID</param>
    /// <returns></returns>
    public int AddNewPlayerGUI(int ID)
    {
        int GUIID = playersGUI.Count;
        playerReadyUps[ID].gameObject.SetActive(true);
        playersGUI.Add(Instantiate(prefabPlayerGUI, playersGUIRoots[GUIID]));
        return GUIID;
    }

    /// <summary>
    /// Sets a player's cooldown fill amount
    /// </summary>
    /// <param name="playerID">The player's ID</param>
    /// <param name="cooldownFill">The player's cooldown fill amount</param>
    public void SetPlayerCooldownFill(int playerID, float cooldownFill)
    {
        playersGUI[playerID].SetPlayerCooldownFill(cooldownFill);
    }

    /// <summary>
    /// Sets a player's candy count
    /// </summary>
    /// <param name="playerID">The player's ID</param>
    /// <param name="candyCount">The player's candy count</param>
    public void SetPlayerCandyCount(int playerID, int candyCount)
    {
        playersGUI[playerID].SetPlayerCandyCount(candyCount);
    }


    public void Click_ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
