using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressToStart : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.instance.Load("game");
    }
}
