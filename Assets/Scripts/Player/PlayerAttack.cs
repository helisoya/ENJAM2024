using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player's attack
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private float attackCooldown = 5;
    [SerializeField] private float attackRadius = 4;
    [SerializeField] private LayerMask attackMask;
    private float lastAttack;

    /// <summary>
    /// Tries to attack if the cooldown permits it
    /// </summary>
    public void TryAttack()
    {
        if (Time.time - lastAttack < attackCooldown) return;

        lastAttack = Time.time;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, attackMask);

        foreach (Collider2D collider in colliders)
        {
            if (collider.attachedRigidbody.gameObject != gameObject)
            {
                // Not me, so die please
                collider.attachedRigidbody.GetComponent<Player>().Stun();
                print(collider.attachedRigidbody.gameObject.name);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
