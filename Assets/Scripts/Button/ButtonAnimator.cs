using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	[ContextMenu("Press")]
	public void Press()
	{
		animator.ResetTrigger("unpress");
		animator.SetTrigger("press");
	}

	[ContextMenu("Unpress")]
	public void Unpress()
	{
		animator.ResetTrigger("press");
		animator.SetTrigger("unpress");
	}
}
