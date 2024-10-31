using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a player's GUI
/// </summary>
public class PlayerGUI : MonoBehaviour
{
    [SerializeField] private Image playerImg;
    [SerializeField] private Image attackCooldownFill;
    [SerializeField] private TextMeshProUGUI candyCountFill;

    void Start()
    {
        SetPlayerCooldownFill(0);
        SetPlayerCandyCount(0);
    }

    /// <summary>
    /// Sets the player's color
    /// </summary>
    /// <param name="color">The player's color</param>
    public void SetPlayerColor(Color color)
    {
        playerImg.color = color;
    }

    /// <summary>
    /// Sets the player's cooldown fill amount
    /// </summary>
    /// <param name="cooldownFill">The player's cooldown fill amount</param>
    public void SetPlayerCooldownFill(float cooldownFill)
    {
        attackCooldownFill.fillAmount = cooldownFill;
    }

    /// <summary>
    /// Sets the player's candy count
    /// </summary>
    /// <param name="candyCount">The player's candy count</param>
    public void SetPlayerCandyCount(int candyCount)
    {
        candyCountFill.text = "x" + candyCount;
    }
}

