using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a player's Ready up component
/// </summary>
public class PlayerReadyUp : MonoBehaviour
{
    [SerializeField] private GameObject readyUpCheck;

    /// <summary>
    /// Sets the ready up check active or not
    /// </summary>
    /// <param name="value">Is the check active ?</param>
    public void SetReadyUpCheckActive(bool value)
    {
        readyUpCheck.SetActive(value);
    }
}
