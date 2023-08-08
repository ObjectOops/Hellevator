using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressToReplay : MonoBehaviour
{
	[SerializeField] private TextMeshPro highscoreText;

    private void Start()
    {
        int highscore = PlayerPrefs.GetInt("trust");
        highscoreText.text = $"Best Trustworthiness: {highscore}";
        AudioManager.instance.PlaySFX("Sound Effect");
    }

    private void OnMouseDown()
	{
		SceneManager.instance.Load("Title");
	}
}
