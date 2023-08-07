using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// There is a Unity component also called "Button", disregard it.
public class Button : MonoBehaviour
{
	public static bool levelSelected = true;
	public static List<Button> buttons = new();

	[SerializeField] private int level;

	private ButtonAnimator buttonAnimator;

	private void Awake()
	{
		buttonAnimator = GetComponent<ButtonAnimator>();
		buttons.Add(this);
	}

	private void OnMouseDown()
	{
		if (!levelSelected)
		{
			buttonAnimator.Press();
			StartCoroutine(ReceiptManager.instance.activeReceipt.Judge(level));
			levelSelected = true;
		}
	}

	public static void ResetButtons()
	{
		foreach(Button button in buttons)
		{
			button.buttonAnimator.Unpress();
		}
	}
}
