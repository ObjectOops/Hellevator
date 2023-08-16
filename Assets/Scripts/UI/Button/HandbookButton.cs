using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandbookButton : MonoBehaviour
{
	[SerializeField] private bool selectForOpen;

	private void OnMouseDown()
	{
		if (selectForOpen)
		{
			UIManager.instance.OpenHandbook();
		}
		else
		{
			UIManager.instance.CloseHandbook();
		}
	}
}
