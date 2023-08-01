using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void Press()
	{
		animator.SetTrigger("press");
	}

	[ContextMenu("Unpress")]
	public void Unpress()
	{
		animator.SetTrigger("unpress");
	}
}
