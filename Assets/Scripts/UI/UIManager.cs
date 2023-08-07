using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	[SerializeField] private TextMeshPro day, trust;
	[SerializeField] private GameObject pauseMenu, transitionScreen;
	[SerializeField] private Animator dayAnimator, trustAnimator;
	[SerializeField] private float notifyDuration, transitionDuration;

	[HideInInspector] public bool paused = false;

	private void Awake()
	{
		instance = this;
		pauseMenu.SetActive(false);
	}

	public void SetDay(int n)
	{
		day.text = $"Day: {n}";
		dayAnimator.SetTrigger("notify");
		Invoke(nameof(StopNotify), notifyDuration);
	}

	public void SetTrust(int n)
	{
		trust.text = $"Trust: {n}";
		trustAnimator.SetTrigger("notify");
		Invoke(nameof(StopNotify), notifyDuration);
	}

	private void StopNotify()
	{
		dayAnimator.SetTrigger("stop");
		trustAnimator.SetTrigger("stop");
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

	public void ShowTransitionScreen(string text)
	{
		transitionScreen.GetComponentInChildren<TextMeshPro>().text = text;
		transitionScreen.SetActive(true);
	}

	public IEnumerator HideTransitionScreen()
	{
		yield return new WaitForSeconds(transitionDuration);
		transitionScreen.SetActive(false);
	}
}
