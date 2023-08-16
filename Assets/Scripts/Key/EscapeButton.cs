using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeButton : MonoBehaviour
{
	private void OnMouseDown()
	{
		if (GameManager.instance.escapeActive)
		{
			SceneManager.instance.Load("Win");
		}
	}
}
