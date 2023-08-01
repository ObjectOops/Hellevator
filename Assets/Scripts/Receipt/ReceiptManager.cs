using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiptManager : MonoBehaviour
{
	public static ReceiptManager instance;

	[SerializeField] private Receipt receiptPrefab;
	[SerializeField] private GameObject receiptSpawn;
	[SerializeField] private List<CrimeCategory> crimeCategories;

	public Receipt activeReceipt;

	private void Start()
	{
		instance = this;

		if (crimeCategories.Count != 9)
		{
			Debug.LogWarning("Crime category count does not equal nine.", this);
		}
	}

	public void GenerateReceipt(bool isBoss)
	{
		Receipt receipt = Instantiate(receiptPrefab);
		int rand1 = Random.Range(1, 10), rand2 = Random.Range(1, 10);
		while (rand1 == rand2)
		{
			rand2 = Random.Range(1, 10);
		}
		CrimeCategory minorLevel = crimeCategories[rand1], majorLevel = crimeCategories[rand2];
		List<string> crimesCommitted = new();

		// One minor crime.
		crimesCommitted.Add(minorLevel.crimes[Random.Range(0, minorLevel.crimes.Count)]);
		if (isBoss)
		{
			// Two major crimes.
			crimesCommitted.Add(majorLevel.crimes[Random.Range(0, majorLevel.crimes.Count)]);
			crimesCommitted.Add(majorLevel.crimes[Random.Range(0, majorLevel.crimes.Count)]);
		}

		// Scramble the order of crimes.
		for (int i = 0; i < crimesCommitted.Count; ++i)
		{
			int swapIndex = Random.Range(0, crimesCommitted.Count);
			string swap = crimesCommitted[swapIndex];
			crimesCommitted[swapIndex] = crimesCommitted[i];
			crimesCommitted[i] = swap;
		}

		foreach (string crime in crimesCommitted)
		{
			receipt.crimes += crime + '\n';
		}
		receipt.level = majorLevel.level;

		activeReceipt = receipt;
		receipt.PrintSequence();
	}

	[System.Serializable] private struct CrimeCategory
	{
		public int level;
		public List<string> crimes;
	}
}
