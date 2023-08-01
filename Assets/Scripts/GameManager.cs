using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	private SpiritManager spiritManager;
	//The day
	public int day;
	//How many spirit has been spawned this day
	public int spiritNum;
	//Timer for testing
	public float testTimer = 9;
	private void Start()
	{
		instance = this;
	}

	private void Update()
	{
		if(spiritManager == null)
		{
            spiritManager = SpiritManager.instance;
        }
		testTimer += Time.deltaTime;
		if(testTimer > 10 )
		{
			testTimer = 0;
			spiritManager.GenerateSpirit();
		}
	}

}
