using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogAnimator : MonoBehaviour
{
	[SerializeField] private float textDelay;

	private Animator animator;
	private TextMeshPro textMesh;

	[HideInInspector] public bool stopNow = false; // Hacked in for feature. Do not expect consistent usage.

	private void Awake()
	{
		textMesh = GetComponentInChildren<TextMeshPro>();
		animator = GetComponent<Animator>();

		Dialog d = GetComponent<Dialog>();
		animator.SetTrigger(d.mephi ? "mephi" : d.spirit ? "spirit" : "player");
	}

	public IEnumerator LinearIn(string text)
	{
		stopNow = false;
		textMesh.text = "";
		foreach (char c in text)
		{
			textMesh.text += c;
			yield return new WaitForSeconds(textDelay);
			if (textMesh.isTextOverflowing)
			{
				textMesh.text = "\n\n\n...";
			}
			if (stopNow)
			{
				stopNow = false;
				break;
			}
		}
	}

	public void Blurb()
	{
		animator.SetTrigger("blurb");
	}

	public void Out()
	{
		animator.SetTrigger("out");
	}
}
