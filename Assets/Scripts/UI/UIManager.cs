using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	[SerializeField] private TextMeshPro day, trust;
	[SerializeField] private GameObject pauseMenu;

	[HideInInspector] public bool paused = false;

	private void Awake()
	{
		instance = this;

		// Ensures that the pause menu is hidden and resolves an odd behavior where buttons are unresponsive on initial load.
		pauseMenu.SetActive(false);
	}

	public void SetDay(int n)
	{
		day.text = $"Day: {n}";
	}

	public void SetTrust(int n)
	{
		trust.text = $"Trust: {n}";
	}

	public void PauseGame()
	{
		Time.timeScale = 0f;
		pauseMenu.SetActive(true);
		paused = true;
	}

	public void ResumeGame()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		paused = false;
	}
}
