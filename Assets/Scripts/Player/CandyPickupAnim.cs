using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a candy picked up by the player when stuning another
/// </summary>
public class CandyPickupAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float destroyAfterSeconds = 1.1f;
    private float speed;
    private Transform target;

    /// <summary>
    /// Initialize the candy
    /// </summary>
    /// <param name="target">The candy's target</param>
    public void Init(Transform target)
    {
        this.target = target;
        speed = Random.Range(9f, 20f);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            animator.SetTrigger("Explode");
            Destroy(gameObject, destroyAfterSeconds);
        }
    }
}
