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
    [SerializeField] private int amountLostOnStun = 2;
    private int score;
    private bool stuned;
    private float stunStart;

    [Header("Components")]
    [SerializeField] private PlayerMovements movements;
    [SerializeField] private PlayerAttack attack;
    [SerializeField] private PlayerInterraction interraction;

    /// <summary>
    /// Gets the player's ID
    /// </summary>
    /// <returns>The player's ID</returns>
    public int GetID()
    {
        return ID;
    }

    /// <summary>
    /// Adds score
    /// </summary>
    /// <param name="scoreAmount">The score to add</param>
    public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
        GameGUI.instance.SetPlayerCandyCount(ID, score);
    }

    /// <summary>
    /// Stuns the player and returns the amount of money stolen if needed
    /// </summary>
    /// <param name="stealMoney">Should money be stolen ?</param>
    /// <returns>The amount of money stolen</returns>
    public int Stun(bool stealMoney = false)
    {
        stunStart = Time.time;
        stuned = true;
        movements.SetVelocity(Vector2.zero);

        if (stealMoney)
        {
            int amountStolen = Mathf.Min(amountLostOnStun, score);
            AddScore(-amountStolen);
            return amountStolen;
        }

        return 0;
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

    void OnAttack(InputValue input)
    {
        if (!stuned)
        {
            attack.TryAttack();
        }
    }

    void OnInterract(InputValue input)
    {
        if (!stuned)
        {
            interraction.TryInterract();
        }
    }

}
