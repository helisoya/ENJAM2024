using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    [SerializeField] private CandyPickupAnim prefabCandyPickup;
    private float lastAttack;

    private FMOD.Studio.EventInstance AttackAudio;


    void Start()
    {
        lastAttack = -attackCooldown;
        AttackAudio = FMODUnity.RuntimeManager.CreateInstance("event:/ATTACK");
    }

    /// <summary>
    /// Tries to attack if the cooldown permits it
    /// </summary>
    public void TryAttack()
    {
        if (Time.time - lastAttack < attackCooldown) return;
        AttackAudio.start();

        lastAttack = Time.time;
        player.SetLightRadius(attackRadius);
        player.SetAnimationTrigger("Attack");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, attackMask);

        foreach (Collider2D collider in colliders)
        {
            Player playerFound = collider.attachedRigidbody.GetComponent<Player>();
            if (playerFound != player)
            {
                // Not me, so die please
                Vector3 vector = collider.bounds.center - transform.position;
                if (!Physics2D.Raycast(transform.position, vector, vector.magnitude, objectsMask))
                {
                    int numberStolen = playerFound.Stun(true);
                    player.AddScore(numberStolen);
                    for (int i = 0; i < numberStolen / 2; i++)
                    {
                        Instantiate(prefabCandyPickup, playerFound.transform.position, Quaternion.identity).Init(transform);
                    }
                }
            }
        }
    }

    void Update()
    {
        float currentCooldownValue = Time.time - lastAttack;
        if (currentCooldownValue < attackCooldown)
        {
            GameGUI.instance.SetPlayerCooldownFill(player.GUIID, currentCooldownValue / attackCooldown);
        }
        else
        {
            GameGUI.instance.SetPlayerCooldownFill(player.GUIID, 1);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
