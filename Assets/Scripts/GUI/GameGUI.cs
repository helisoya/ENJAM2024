using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the Game's GUI
/// </summary>
public class GameGUI : MonoBehaviour
{
    public static GameGUI instance { get; private set; }

    [Header("Players")]
    [SerializeField] private PlayerGUI[] playersGUI;

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