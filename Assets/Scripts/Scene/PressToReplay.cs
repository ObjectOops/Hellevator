using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressToReplay : MonoBehaviour
{
	[SerializeField] private TextMeshPro highscoreText;
	[SerializeField] private float timeBuffer = 5f; // For some reason the mouse press event from the previous scene persists.

	private void Start()
	{
		int highscore = PlayerPrefs.GetInt("trust");
		highscoreText.text = $"Best Trustworthiness: {highscore}";
		AudioManager.instance.PlaySFX("Sound Effect");
	}

	private void OnMouseDown()
	{
		if (Time.timeSinceLevelLoad > timeBuffer)
		{
			SceneManager.instance.Load("Title");
		}
	}
}
