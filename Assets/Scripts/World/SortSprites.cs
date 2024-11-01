using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sorts sprites by the y axis
/// </summary>
public class SortSprites : MonoBehaviour
{

    void Update()
    {
        SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();

        if (allSprites.Length <= 1) return;

        List<SpriteRenderer> sprites = new List<SpriteRenderer>(allSprites);

        sprites.Sort((o1, o2) => (
            o2.transform.position.y.CompareTo(o1.transform.position.y)
            ));

        for (int i = 0; i < sprites.Count; i++)
        {
            sprites[i].sortingOrder = i + 1;
        }
    }
}
