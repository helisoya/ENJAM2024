using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a door
/// </summary>
public class Door : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    private List<GameObject> objectsIn;

    void Start()
    {
        objectsIn = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        print(collider.attachedRigidbody.gameObject.name);
        objectsIn.Add(collider.attachedRigidbody.gameObject);

        if (objectsIn.Count == 1)
        {
            // Was at 0 before, so must be opened
            spriteRenderer.sprite = openSprite;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        print(collider.attachedRigidbody.gameObject.name);
        objectsIn.Remove(collider.attachedRigidbody.gameObject);

        if (objectsIn.Count == 0)
        {
            // Is at 0 , so must be closed
            spriteRenderer.sprite = closedSprite;
        }
    }
}
