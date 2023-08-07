using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
	public static Dialog spiritBox, playerBox, mephiBox;

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

	public IEnumerator Speak(string speech)
	{
		dialogAnimator.Blurb();
		yield return dialogAnimator.LinearIn(speech);
	}

	[ContextMenu("Test End")]
	public void End()
	{
		dialogAnimator.Out();
	}

	[ContextMenu("Test Speak")]
	private void TestSpeak()
	{
		StartCoroutine(Speak("Test."));
	}
}
