using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// There is a Unity component also called "Button", disregard it.
public class Button : MonoBehaviour
{
	public static bool levelSelected = true;
	private static readonly List<Button> buttons = new();

	[SerializeField] private int level;

	private ButtonAnimator buttonAnimator;

	private const int normalButtonCount = 9;

	private void Awake()
	{
		buttonAnimator = GetComponent<ButtonAnimator>();
		
		if (buttons.Count == normalButtonCount) // Extremely important! Static references carry over between scenes!
		{
			buttons.Clear(); // Ensures that old buttons are cleared on game replay.
		}
		
		buttons.Add(this);
	}

	private void OnMouseDown()
	{
		if (!levelSelected && !UIManager.instance.paused)
		{
			AudioManager.instance.PlaySFX("Button Press");
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
