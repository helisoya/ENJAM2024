using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Represents a player's score in the end screen
/// </summary>
public class PlayerScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    /// <summary>
    /// Sets the component's score
    /// </summary>
    /// <param name="score">The score</param>
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
