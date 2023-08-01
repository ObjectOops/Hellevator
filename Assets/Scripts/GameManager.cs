using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	//The day
	public int day;
	//How many spirit has been spawned this day
	public int spiritNum;

	private void Start()
	{
		instance = this;
	}

	private void Update()
	{
		
	}

}
