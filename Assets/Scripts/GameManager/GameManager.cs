using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool InGame { get; private set; }

    [Header("Timer")]
    [SerializeField] private float gameTimeInSeconds;

    [Header("Players")]
    [SerializeField] private Player[] players;

    void Awake()
    {
        instance = this;
        InGame = true;
    }

    void Update()
    {
        gameTimeInSeconds -= Time.deltaTime;

        if (gameTimeInSeconds <= 0)
        {
            // End
            InGame = false;
        }
        else
        {
            GameGUI.instance.SetTimerValue((int)gameTimeInSeconds);
        }
    }
}
