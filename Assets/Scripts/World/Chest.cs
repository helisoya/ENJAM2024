using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a chest
/// </summary>
public class Chest : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private Animator animator;
    [SerializeField] private int minCandy = 2;
    [SerializeField] private int maxCandy = 10;
    [SerializeField] private float cooldownTime = 30;
    [SerializeField] private float probabilityOfTrap = 0.2f;
    private bool open;
    private float openStart;

    private FMOD.Studio.EventInstance _chestAudio;
    private FMOD.Studio.EventInstance _chestTrickAudio;
    void Start()
    {
        open = false;
        _chestAudio = FMODUnity.RuntimeManager.CreateInstance("event:/CHEST");
        _chestTrickAudio = FMODUnity.RuntimeManager.CreateInstance("event:/STUN");
    }

    void Update()
    {
        if (open && Time.time - openStart >= cooldownTime)
        {
            open = false;
            animator.SetTrigger("Close");
        }
    }

    /// <summary>
    /// Checks if the chest can be opened
    /// </summary>
    /// <returns>Can the chest be opened ?</returns>
    public bool CanOpen()
    {
        return !open;
    }

    /// <summary>
    /// Opens the chest
    /// </summary>
    /// <param name="player">The player that opened the chest</param>
    /// <returns>The amount of candy in the chest, -1 means it was trapped</returns>
    public int Open(Player player)
    {
        open = true;
        _chestAudio.start();
        openStart = Time.time;

        if (player.canBeTargetedByBuendia && Random.Range(0f, 1f) <= probabilityOfTrap)
        {
            player.SetCanBeTargetedByBudendia(false);
            animator.SetTrigger("Trick");
            _chestTrickAudio.start();

            return -1;
        }
        player.SetCanBeTargetedByBudendia(true);
        animator.SetTrigger("Treat");
        return Random.Range(minCandy, maxCandy);
    }
}
