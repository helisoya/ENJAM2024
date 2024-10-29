using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player's movements
/// </summary>
public class PlayerMovements : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private float speed;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    private Vector2 velocity;

    /// <summary>
    /// Sets the player's velocity
    /// </summary>
    /// <param name="velocity">The new velocity</param>
    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }

    void FixedUpdate()
    {
        rb.velocity = velocity * speed;
    }
}