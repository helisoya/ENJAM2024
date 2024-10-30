using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the game's flickering lights
/// </summary>
public class FlickeringLights : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private float flickerCooldown;
    [SerializeField] private int maxActiveLights;
    private float lastFlicker;

    void Start()
    {
        GenerateFlickerArangement();
    }

    void Update()
    {
        if (Time.time - lastFlicker >= flickerCooldown)
        {
            GenerateFlickerArangement();
        }
    }

    /// <summary>
    /// Generates a new flicker arrangement
    /// </summary>
    private void GenerateFlickerArangement()
    {
        lastFlicker = Time.time;
        List<int> allLights = new List<int>();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            allLights.Add(i);
        }

        int currentCount = 0;
        int randIdx;
        while (currentCount < maxActiveLights && allLights.Count > 0)
        {
            randIdx = Random.Range(0, allLights.Count);
            transform.GetChild(allLights[randIdx]).gameObject.SetActive(true);
            allLights.RemoveAt(randIdx);
            currentCount++;
        }
    }
}
