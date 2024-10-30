using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class AudioManager : MonoBehaviour
{
    //singleton
    public static AudioManager Instance { get; private set; }
    // Start is called before the first frame update

    private FMOD.Studio.EventInstance _levelAmbianceAudio;

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
    }
    void Start()
    {
        _levelAmbianceAudio.start();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
