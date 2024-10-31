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

    private FMOD.Studio.EventInstance ChestAudio;
    void Start()
    {
        open = false;
        ChestAudio = FMODUnity.RuntimeManager.CreateInstance("event:/CHEST");
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
    /// <returns>The amount of candy in the chest, -1 means it was trapped</returns>
    public int Open()
    {
        open = true;
        ChestAudio.start();
        animator.SetTrigger("Toggle");
        openStart = Time.time;

        if (Random.Range(0f, 1f) <= probabilityOfTrap)
        {
            animator.SetTrigger("Trick");
            return -1;
        }

        animator.SetTrigger("Treat");
        return Random.Range(minCandy, maxCandy);
    }
}
