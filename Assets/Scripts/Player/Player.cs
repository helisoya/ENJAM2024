using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents a player
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private int ID;
    private int score;

    [Header("Components")]
    [SerializeField] private PlayerMovements movements;

    /// <summary>
    /// Adds score
    /// </summary>
    /// <param name="score">The score to add</param>
    public void AddScore(int score)
    {
        this.score += score;
    }

    void OnMove(InputValue input)
    {
        movements.SetVelocity(input.Get<Vector2>());
    }

}
