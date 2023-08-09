using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If we're doing everything without a Canvas, this might as well exist.
public class PauseResumeButton : MonoBehaviour
{
	[SerializeField] private bool selectForPause;

	private void OnMouseDown()
	{
		if (selectForPause)
		{
			UIManager.instance.PauseGame();
		}
		else
		{
			UIManager.instance.ResumeGame();
		}
	}
}
