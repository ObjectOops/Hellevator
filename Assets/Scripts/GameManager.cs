using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	// Insert references to other scenes.
	// Do not hide in inspector. Caution, default values may be overwritten in the inspector.
	public int day = 1, trust = 500, judged = 0, limit = 5;

    private void Awake()
    {
		Application.targetFrameRate = 30;
		Time.timeScale = 3; // Testing only!
		instance = this;
    }

    private void Start()
	{
		// Initialize UI.
		Debug.Log("Starting NextSpirit coroutine.", this);
		StartCoroutine(NextSpirit());
	}

	public IEnumerator NextDay()
    {
		++day;
		judged = 0;
		UIManager.instance.SetDay(day);
		// Key assembly, updating the UI, going to the win scene, and general transition screens.
		yield return null; // Temporary.
    }

	public IEnumerator NextSpirit()
    {
		Button.ResetButtons();
		Debug.Log("Starting GenerateSpirit coroutine", this);
		yield return SpiritManager.instance.GenerateSpirit(day, judged);
		bool boss = judged == limit - 1;
		Debug.Log("BOSS???: " + boss + ' ' + judged + ' ' + limit, this);
		ReceiptManager.instance.GenerateReceipt(boss);
		++judged;
		if (boss)
        {
			yield return NextDay();
        }
    }

	public void SetTrust(int newTrust)
    {
		trust = newTrust;
		TestTrust();
		UIManager.instance.SetTrust(trust);
    }

	private void TestTrust()
    {
		if (trust <= 0)
        {
			// Go to game over scene.
        }
    }

	// Go to game over scene function.
}
