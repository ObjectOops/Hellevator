using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : MonoBehaviour
{
	public void GooseGone()
	{
		AudioManager.instance.PlayMusic("Intro Theme");
	}
}
