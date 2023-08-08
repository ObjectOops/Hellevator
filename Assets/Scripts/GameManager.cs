using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public string playerName = "Dante", mephiName = "Mephistopheles"; // May be overwritten in the inspector.

	// Insert references to other scenes.
	// Do not hide in inspector. Caution, default values may be overwritten in the inspector.
	public int day = 1, trust = 500, judged = 0, limit = 5, lastDay = 4;
	public bool boss = false;

	private void Awake()
	{
		instance = this;
		Application.targetFrameRate = 30;
		// Time.timeScale = 10; // For testing only!
	}

	private void Start()
	{
		UIManager.instance.SetDay(day);
		UIManager.instance.SetTrust(trust);
		AudioManager.instance.Start();
		AudioManager.instance.PlayMusic("Background Music");
		AudioManager.instance.PlaySFX("Enter Game");
		StartCoroutine(NextSpirit());
	}

	public IEnumerator NextDay()
	{
		++day;
		judged = 0;

		string[] mephiIntermezzo = SpiritManager.instance.mephiIntermezzo;
		AudioClip[] mephiVoiceoverIntermezzov = SpiritManager.instance.mephiVoiceoverIntermezzo;
		yield return Dialog.mephiBox.Speak($"\n{mephiName}\n\n{mephiIntermezzo[day - 2]}", mephiVoiceoverIntermezzov[day - 2]);
		Dialog.mephiBox.End();

		if (day == lastDay)
		{
			int bestTrust = Mathf.Max(trust, PlayerPrefs.GetInt("trust", 0));
			PlayerPrefs.SetInt("trust", bestTrust);
			SceneManager.instance.Load("Win");
		}

		UIManager.instance.ShowTransitionScreen($"Day: {day}");
		UIManager.instance.SetDay(day);
		yield return UIManager.instance.HideTransitionScreen();
	}

	public IEnumerator NextSpirit()
	{
		Button.ResetButtons();
		yield return SpiritManager.instance.GenerateSpirit(day, judged);
		boss = judged == limit - 1;
		ReceiptManager.instance.GenerateReceipt(boss);
		Button.levelSelected = false;
		++judged;
	}

	public void SetTrust(int newTrust)
	{
		trust = newTrust;
		TestTrust();
		UIManager.instance.SetTrust(trust);
	}

	private void TestTrust()
	{
		if (trust <= 0)
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		SceneManager.instance.Load("Lose");
	}
}
