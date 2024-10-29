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
    private bool open;
    private float openStart;

    void Start()
    {
        open = false;
    }

    void Update()
    {
        if (open && Time.time - openStart >= cooldownTime)
        {
            open = false;
            animator.SetTrigger("Toggle");
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
    /// <returns>The amount of candy in the chest</returns>
    public int Open()
    {
        open = true;
        animator.SetTrigger("Toggle");
        openStart = Time.time;
        return Random.Range(minCandy, maxCandy);
    }
}
