using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the main menu GUI
/// </summary>
public class MainMenuGUI : MonoBehaviour
{
    public void Click_Start()
    {
        SceneManager.LoadScene("Game");
    }
}
