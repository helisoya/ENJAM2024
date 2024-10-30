using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player interraction
/// </summary>
public class PlayerInterraction : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private float interractRadius = 4;
    [SerializeField] private LayerMask interractMask;
    [SerializeField] private LayerMask objectsMask;
    [SerializeField] private Player player;

    /// <summary>
    /// Tries to interract with a chest
    /// </summary>
    public void TryInterract()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interractRadius, interractMask);
        Chest chest; ;
        foreach (Collider2D collider in colliders)
        {
            chest = collider.GetComponent<Chest>();
            Vector3 vector = collider.transform.position - transform.position;

            if (chest != null && chest.CanOpen() && !Physics2D.Raycast(transform.position, vector, vector.magnitude, objectsMask))
            {
                int score = chest.Open();
                if (score == -1)
                {
                    player.Stun();
                }
                else
                {
                    player.AddScore(score);
                }

            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interractRadius);
    }
}
