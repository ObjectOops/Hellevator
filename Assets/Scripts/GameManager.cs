using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	private void Start()
	{
		instance = this;
	}

	private void Update()
	{
		
	}
}
