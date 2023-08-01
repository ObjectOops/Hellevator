using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Button : MonoBehaviour
{
	public static bool levelSelected = false;

	private ButtonAnimator buttonAnimator;

	private void Start()
	{
		buttonAnimator = GetComponent<ButtonAnimator>();
	}

	public void OnMouseDown()
	{
		if (!levelSelected)
		{
			buttonAnimator.Press();
			ReceiptManager.instance.activeReceipt.Judge(this.name);
			levelSelected = true;
		}
	}
}
