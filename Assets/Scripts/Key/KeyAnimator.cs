using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyAnimator : MonoBehaviour
{
	public static KeyAnimator instance;

	[SerializeField] private TextMeshPro label;
	[SerializeField] private Color labelColor;
	[SerializeField] private GameObject shade;
	[SerializeField] private Animator[] sequentialAnimators;
	[SerializeField] private ParticleSystem particles;
	[SerializeField] private string unlockLabelText;
	[SerializeField] private float shadeDuration;

	private Animator animator;

	private int animatorIndex = 0;

	private void Awake()
	{
		instance = this;
		animator = GetComponent<Animator>();
		shade.SetActive(false);
	}

	public IEnumerator NextObtain()
	{
		shade.SetActive(true);
		sequentialAnimators[animatorIndex].SetTrigger("obtain");
		++animatorIndex;

		yield return new WaitForSeconds(shadeDuration);
		shade.SetActive(false);
	}

	public IEnumerator AllBye()
	{
		foreach (Animator animator in sequentialAnimators)
		{
			animator.SetTrigger("bye");
		}

		yield return new WaitForSeconds(shadeDuration);
	}

	public IEnumerator Unlock()
	{
		shade.SetActive(true);
		particles.Play();
		animator.SetTrigger("make");
		yield return new WaitForSeconds(shadeDuration);
		shade.SetActive(false);
		animator.SetTrigger("unlock");
		label.text = unlockLabelText;
		label.color = labelColor;
	}
}
