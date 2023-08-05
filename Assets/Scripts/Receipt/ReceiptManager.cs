using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiptManager : MonoBehaviour
{
	public static ReceiptManager instance;

	[SerializeField] private GameObject receiptPrefab;
	[SerializeField] private Transform receiptSpawn;
	[SerializeField] private List<CrimeCategory> crimeCategories;

	// May need under some circumstances.
	[SerializeField] private int receiptCharacterLengthMax;

	[HideInInspector] public Receipt activeReceipt;

	public SpriteRenderer[] levelBackground; // note

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		if (crimeCategories.Count != 9)
		{
			Debug.LogWarning("Crime category count does not equal nine.", this);
		}
	}

	public void GenerateReceipt(bool isBoss)
	{
		GameObject receiptObject = Instantiate(receiptPrefab);
		Receipt receipt = receiptObject.GetComponent<Receipt>();
		receipt.transform.SetPositionAndRotation(receiptSpawn.position, receiptSpawn.rotation);
		receipt.transform.localScale = receiptSpawn.localScale;

		// [2, 10) --> for each of the nine levels *with crimes*.
		int rand1 = Random.Range(1, 9), rand2 = Random.Range(1, 9);
		while (rand1 == rand2)
		{
			rand2 = Random.Range(1, 9);
		}
		CrimeCategory minorLevel = crimeCategories[rand1], majorLevel = crimeCategories[rand2];
		List<string> crimesCommitted = new();

		// One minor crime.
		crimesCommitted.Add(minorLevel.crimes[Random.Range(0, minorLevel.crimes.Count)]);
		receipt.level = minorLevel.level;
		if (isBoss)
		{
			// Two major crimes.
			crimesCommitted.Add(majorLevel.crimes[Random.Range(0, majorLevel.crimes.Count)]);
			crimesCommitted.Add(majorLevel.crimes[Random.Range(0, majorLevel.crimes.Count)]);
			receipt.level = majorLevel.level;
		}

		// Scramble the order of crimes.
		for (int i = 0; i < crimesCommitted.Count; ++i)
		{
			int swapIndex = Random.Range(0, crimesCommitted.Count);
			string swap = crimesCommitted[swapIndex];
			crimesCommitted[swapIndex] = crimesCommitted[i];
			crimesCommitted[i] = swap;
		}

		receipt.finePrint += 
$@"{SpiritManager.instance.activeSpirit.realName}
> {SpiritManager.instance.activeSpirit.description}
> {"Death: " + SpiritManager.instance.activeSpirit.demise}
---------------------";
		foreach (string crime in crimesCommitted)
		{
			receipt.finePrint += $"\n{"Crime: " + crime}";
		}

		activeReceipt = receipt;
		receipt.PrintSequence();
	}

	// May need under some circumstances.
	private string BreakString(string str)
    {
		string result = "";
		for (int i = 0; i < str.Length; ++i)
		{
			result += str[i];
			if (i % receiptCharacterLengthMax == 0)
			{
				result += '\n';
			}
		}
		return result;
	}

	[System.Serializable] private struct CrimeCategory
	{
		public int level;
		public List<string> crimes;
	}
}
