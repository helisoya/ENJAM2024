using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool InGame { get; private set; }
    private List<int> readyUps;

    [Header("Timer")]
    [SerializeField] private float gameTimeInSeconds;

    [Header("Players")]
    [SerializeField] private Transform[] spawnPositions;
    public List<Player> players { get; private set; }

    private FMOD.Studio.EventInstance _uiAudio;

    void Awake()
    {
        _uiAudio = FMODUnity.RuntimeManager.CreateInstance("event:/UI");
        instance = this;
        InGame = false;
        players = new List<Player>();
        readyUps = new List<int>();
    }

    /// <summary>
    /// Registers a new player
    /// </summary>
    /// <param name="player">The new player</param>
    /// <returns>The new player's ID</returns>
    public int RegisterPlayer(Player player)
    {
        _uiAudio.start();
        int ID = players.Count;
        players.Add(player);
        player.transform.position = spawnPositions[ID].position;
        return ID;
    }

    /// <summary>
    /// Ready up a player
    /// </summary>
    /// <param name="ID">The player's ID</param>
    /// <param name="GUIID">The player's GUIID</param>
    public void ReadyUp(int ID)
    {
        if (!readyUps.Contains(ID))
        {
            _uiAudio.start();
            readyUps.Add(ID);
            GameGUI.instance.ReadyUpPlayer(ID);
            if (readyUps.Count >= players.Count)
            {
                InGame = true;
                GameGUI.instance.OpenGamePlayScreen();
            }
        }
    }

    void Update()
    {
        if (!InGame) return;
        gameTimeInSeconds -= Time.deltaTime;

        if (gameTimeInSeconds <= 0)
        {
            // End
            InGame = false;
            GameGUI.instance.OpenEndScreen(players);
        }
        else
        {
            GameGUI.instance.SetTimerValue((int)gameTimeInSeconds);
        }
    }
}
