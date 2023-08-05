using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressToStart : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayMusic("Intro Theme");
    }

    private void OnMouseDown()
    {
        if (!UIManager.instance.paused)
        {
            SceneManager.instance.Load("game");
        }
    }
}
