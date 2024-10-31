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
    [SerializeField] private float closeAfterSeconds = 1f;
    private List<GameObject> objectsIn;
    private bool open;
    private float startClose;

    private FMOD.Studio.EventInstance _doorOpenAudio;

    void Start()
    {
        objectsIn = new List<GameObject>();
        _doorOpenAudio = FMODUnity.RuntimeManager.CreateInstance("event:/OPEN DOOR");
    }

    void Update()
    {
        if (open && objectsIn.Count == 0 && Time.time - startClose >= closeAfterSeconds)
        {
            spriteRenderer.sprite = closedSprite;
            open = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        objectsIn.Add(collider.attachedRigidbody.gameObject);

        if (objectsIn.Count == 1)
        {
            open = true;
            // Was at 0 before, so must be opened
            spriteRenderer.sprite = openSprite;
            _doorOpenAudio.start();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        objectsIn.Remove(collider.attachedRigidbody.gameObject);

        if (objectsIn.Count == 0)
        {
            // Is at 0 , so must be closed after x seconds
            startClose = Time.time;
        }
    }
}
