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
    [SerializeField] private Animator animator;
    [SerializeField] private float destroyAfterSeconds = 1.1f;
    private bool alreadyActivated = false;

    private FMOD.Studio.EventInstance _candyAudio;

    private void Awake()
    {
        _candyAudio = FMODUnity.RuntimeManager.CreateInstance("event:/CANDY");
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.attachedRigidbody.GetComponent<Player>();
        if (!alreadyActivated && player)
        {
            alreadyActivated = true;
            player.AddScore(scoreValue);
            animator.SetTrigger("Explode");
            _candyAudio.start();
            Destroy(gameObject, destroyAfterSeconds);
        }
    }
}
