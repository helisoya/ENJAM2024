using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //singleton
    public static AudioManager Instance { get; private set; }
    //[SerializeField] private Player _player1;
    //[SerializeField] private Player _player2;
    // Start is called before the first frame update

    private FMOD.Studio.EventInstance _levelAmbianceAudio;
    private FMOD.Studio.EventInstance _levelMusicAudio;
    private FMOD.Studio.EventInstance _player1Movement;
    private FMOD.Studio.EventInstance _player2Movement;

    private bool _canPlayer1AudioPlay = true;
    private bool _canPlayer2AudioPlay = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        _levelAmbianceAudio = FMODUnity.RuntimeManager.CreateInstance("event:/AMBIANCE");
        _player1Movement = FMODUnity.RuntimeManager.CreateInstance("event:/WALK PLAYER 1");
        _player2Movement = FMODUnity.RuntimeManager.CreateInstance("event:/WALK PLAYER 2");
        _levelMusicAudio = FMODUnity.RuntimeManager.CreateInstance("event:/MUSIC");
    }
    void Start()
    {
        _levelAmbianceAudio.start();
        _levelMusicAudio.start();

    }

    // Update is called once per frame
    void Update()
    {
        //if (_player1.GetComponent<Rigidbody2D>().velocity != Vector2.zero && _canPlayer1AudioPlay)
        //{
        //    _player1Movement.start();
        //    _canPlayer1AudioPlay = false;

        //}

        //if (_player1.GetComponent<Rigidbody2D>().velocity == Vector2.zero && _canPlayer1AudioPlay == false)
        //{
        //    _canPlayer1AudioPlay = true;
        //    _player1Movement.stop(STOP_MODE.ALLOWFADEOUT);


        //}

        //if (_player2.GetComponent<Rigidbody2D>().velocity != Vector2.zero && _canPlayer2AudioPlay)
        //{
        //    _player2Movement.start();
        //    _canPlayer2AudioPlay = false;
        //    Debug.Log("Audio Start");
        //}

        //if (_player2.GetComponent<Rigidbody2D>().velocity == Vector2.zero && _canPlayer2AudioPlay == false)
        //{
        //    _canPlayer2AudioPlay = true;
        //    _player2Movement.stop(STOP_MODE.ALLOWFADEOUT);
        //    Debug.Log("Audio Stop");
        //}
    }


}
