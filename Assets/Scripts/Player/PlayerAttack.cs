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
    [SerializeField] private LayerMask objectsMask;
    [SerializeField] private Player player;
    private float lastAttack;

    void Start()
    {
        lastAttack = -attackCooldown;
    }

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
                Vector3 vector = collider.transform.position - transform.position;
                if (!Physics2D.Raycast(transform.position, vector, vector.magnitude, objectsMask))
                {
                    player.AddScore(collider.attachedRigidbody.GetComponent<Player>().Stun(true));
                    print(collider.attachedRigidbody.gameObject.name);
                }
            }
        }
    }

    void Update()
    {
        float currentCooldownValue = Time.time - lastAttack;
        if (currentCooldownValue < attackCooldown)
        {
            GameGUI.instance.SetPlayerCooldownFill(player.GetID(), 1 - (currentCooldownValue / attackCooldown));
        }
        else
        {
            GameGUI.instance.SetPlayerCooldownFill(player.GetID(), 0);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
