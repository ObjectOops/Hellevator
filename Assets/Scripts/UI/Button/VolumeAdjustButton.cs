using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAdjustButton : MonoBehaviour
{
	private static List<VolumeAdjustButton> buttons = new();

	[SerializeField] private string mixerGroup;
	[SerializeField] private float volumeLevel;

	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		buttons.Add(this);
	}

	public void OnEnable()
	{
		if ((mixerGroup == AudioManager.GROUP_NAMES[0] && volumeLevel == AudioManager.instance.GetVolume(mixerGroup)) ||
			(mixerGroup == AudioManager.GROUP_NAMES[1] && volumeLevel == AudioManager.instance.GetVolume(mixerGroup)) ||
			(mixerGroup == AudioManager.GROUP_NAMES[2] && volumeLevel == AudioManager.instance.GetVolume(mixerGroup)))
		{
			animator.SetTrigger("press");
		}
	}

	private void OnMouseDown()
	{
		AudioManager.instance.AdjustVolume(mixerGroup, volumeLevel);
		foreach (VolumeAdjustButton button in buttons)
		{
			if (button.transform.parent == this.transform.parent)
			{
				button.animator.ResetTrigger("press");
				button.animator.SetTrigger("unpress");
			}
		}
		animator.ResetTrigger("unpress");
		animator.SetTrigger("press");
	}
}
