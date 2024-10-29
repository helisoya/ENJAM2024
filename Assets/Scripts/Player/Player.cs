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
    [SerializeField] private float stunLength = 3;
    private int score;
    private bool stuned;
    private float stunStart;

    [Header("Components")]
    [SerializeField] private PlayerMovements movements;
    [SerializeField] private PlayerAttack attack;

    /// <summary>
    /// Adds score
    /// </summary>
    /// <param name="score">The score to add</param>
    public void AddScore(int score)
    {
        this.score += score;
        print("Player " + ID + " score is now : " + this.score);
    }

    /// <summary>
    /// Stuns the player
    /// </summary>
    public void Stun()
    {
        stunStart = Time.time;
        stuned = true;
        movements.SetVelocity(Vector2.zero);
    }

    void Update()
    {
        if (stuned && Time.time - stunStart >= stunLength)
        {
            stuned = false;
        }
    }

    void OnMove(InputValue input)
    {
        if (!stuned)
        {
            movements.SetVelocity(input.Get<Vector2>());
        }

    }

    void OnFire(InputValue input)
    {
        if (!stuned)
        {
            attack.TryAttack();
        }
    }

}
