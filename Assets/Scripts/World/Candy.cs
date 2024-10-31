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
    [SerializeField] private float hideAfterSeconds = 1.1f;
    [SerializeField] private float minRespawnTime = 10;
    [SerializeField] private float maxRespawnTime = 10;
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
            _candyAudio.start();
            GameManager.instance.StartCoroutine(Routine_Respawn());
        }
    }

    IEnumerator Routine_Respawn()
    {
        animator.SetTrigger("Explode");
        yield return new WaitForSeconds(hideAfterSeconds);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));
        gameObject.SetActive(true);
        alreadyActivated = false;
    }
}
