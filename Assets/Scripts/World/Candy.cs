using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a candy on the floor
/// </summary>
public class Candy : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private int scoreValue = 1;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.attachedRigidbody.GetComponent<Player>();
        if (player)
        {
            print(scoreValue);
            player.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
