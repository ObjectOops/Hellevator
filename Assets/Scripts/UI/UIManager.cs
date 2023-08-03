using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshPro day;
    public TextMeshPro trust;

    private void Awake()
    {
        instance = this;
    }

    public void SetDay(int input)
    {
        day.text = "Day: " + input.ToString();
    }

    public void SetTrust(int input)
    {
        trust.text = "Trust: " + input.ToString();
    }
}
