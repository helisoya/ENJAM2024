using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the thunder in the game
/// </summary>
public class Thunder : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private Animator animator;
    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;
    private float lastThunder;
    private float currentThunderCooldown;

    private FMOD.Studio.EventInstance ThunderStrike;
    void Start()
    {
        GenerateNextThunder();
        ThunderStrike = FMODUnity.RuntimeManager.CreateInstance("event:/THUNDERS");
    }

    /// <summary>
    /// Generates when will the next thunder activate
    /// </summary>
    private void GenerateNextThunder()
    {
        lastThunder = Time.time;
        currentThunderCooldown = Random.Range(minRange, maxRange);
    }

    void Update()
    {
        if (Time.time - lastThunder >= currentThunderCooldown)
        {
            animator.SetTrigger("Thunder");
            ThunderStrike.start();
            GenerateNextThunder();
        }
    }
}
