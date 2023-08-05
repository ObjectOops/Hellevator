using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// There is a Unity component also called "Button", disregard it.
public class Button : MonoBehaviour
{
	public static bool levelSelected = false;
	public static List<Button> buttons = new();

	[SerializeField] private int level;

	private ButtonAnimator buttonAnimator;

	private void Start()
	{
		buttonAnimator = GetComponent<ButtonAnimator>();
		buttons.Add(this);
	}

	public void OnMouseDown()
	{
		if (!levelSelected)
		{
			buttonAnimator.Press();
			StartCoroutine(ReceiptManager.instance.activeReceipt.Judge(level));
			levelSelected = true;
			Color swap = ReceiptManager.instance.levelBackground[level].color;
			swap.a = 1;
			ReceiptManager.instance.levelBackground[level].color = swap;
            //Instantiate(ReceiptManager.instance.levelBackground[level]);
        }
	}

	public static void ResetButtons()
	{
		foreach(Button button in buttons)
        {
			button.buttonAnimator.Unpress();
        }
		levelSelected = false;
	}
}
