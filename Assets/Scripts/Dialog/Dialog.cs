using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
	public static Dialog spiritBox, playerBox, mephiBox;

	[SerializeField] private float dialogBuffer;

	public bool spirit, mephi;

	private DialogAnimator dialogAnimator;

	private void Awake()
	{
		dialogAnimator = GetComponent<DialogAnimator>();
		if (mephi)
		{
			mephiBox = this;
		}
		else if (spirit)
		{
			spiritBox = this;
		}
		else
		{
			playerBox = this;
		}
	}

	public IEnumerator Speak(string speech, AudioClip voiceover)
	{
		dialogAnimator.Blurb();
		StartCoroutine(dialogAnimator.LinearIn(speech));
		yield return AudioManager.instance.PlayDialog(voiceover);
		yield return new WaitForSeconds(dialogBuffer);
	}

	private void OnMouseDown()
	{
		// Day two has unskippable intermezzo dialog. Day counter is 3 by this point. Boss not yet reset.
		// Has the consequence of also making the last boss's dialog unskippable, which is acceptable.
		// Best not to risk changing the order of execution of game manager attribute assignments at this time.
		if (!(GameManager.instance.day == 3 && GameManager.instance.boss))
		{
			dialogAnimator.stopNow = true;
			AudioManager.instance.StopDialog();
		}
	}

	[ContextMenu("Test End")]
	public void End()
	{
		dialogAnimator.Out();
	}

	[ContextMenu("Test Speak")]
	private void TestSpeak()
	{
		StartCoroutine(Speak("Test.", null));
	}
}
