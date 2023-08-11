using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiptManager : MonoBehaviour
{
	public static ReceiptManager instance;

	[SerializeField] private GameObject receiptPrefab;
	[SerializeField] private Transform receiptSpawn;
	[SerializeField] private CrimeCategory[] crimeCategories;

	// May need under some circumstances.
	// [SerializeField] private int receiptCharacterLengthMax;

	[HideInInspector] public Receipt activeReceipt;

	private readonly HashSet<string> usedCrimes = new();

	// [2, 10) --> for each of the nine levels *with crimes*.
	private const int lowerBound = 1, upperBound = 9;

	private void Awake()
	{
		instance = this;
		if (crimeCategories.Length != 9)
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

		KeyValuePair<int, string> minorPair = GetUniqueCrime();
		int minorLevel = minorPair.Key;
		string minorCrime = minorPair.Value;

		List<string> crimesCommitted = new();

		// One minor crime.
		crimesCommitted.Add(minorCrime);
		receipt.level = minorLevel;

		if (isBoss)
		{
			KeyValuePair<int, string> majorPair1 = GetUniqueCrime(excludeLevel: minorLevel);
			int majorLevel = majorPair1.Key;
			string majorCrime1 = majorPair1.Value;
			KeyValuePair<int, string> majorPair2 = GetUniqueCrime(forceLevel: majorLevel, excludeCrime: majorCrime1);
			string majorCrime2 = majorPair2.Value;

			// Two major crimes.
			crimesCommitted.Add(majorCrime1);
			crimesCommitted.Add(majorCrime2);
			receipt.level = majorLevel;
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

	// There should always be enough crimes.
	private KeyValuePair<int, string> GetUniqueCrime(int excludeLevel = -1, int forceLevel = -1, string excludeCrime = null)
	{
		if (forceLevel != -1)
		{
			CrimeCategory forceCategory = crimeCategories[forceLevel - 1];
			string forceCrime = forceCategory.crimes[Random.Range(0, forceCategory.crimes.Length)];
			while (forceCrime == excludeCrime)
			{
				forceCrime = forceCategory.crimes[Random.Range(0, forceCategory.crimes.Length)];
			}
			usedCrimes.Add(forceCrime);
			return new KeyValuePair<int, string>(forceLevel, forceCrime);
		}

		int randLevel = Random.Range(lowerBound, upperBound);
		while (LevelUsedUp(randLevel) || 
			   crimeCategories[randLevel].level == excludeLevel)
		{
			randLevel = Random.Range(lowerBound, upperBound);
		}
		CrimeCategory category = crimeCategories[randLevel];
		string randCrime = category.crimes[Random.Range(0, category.crimes.Length)];
		while (usedCrimes.Contains(randCrime))
		{
			randCrime = category.crimes[Random.Range(0, category.crimes.Length)];
		}
		usedCrimes.Add(randCrime);
		return new KeyValuePair<int, string>(category.level, randCrime);
	}

	private bool LevelUsedUp(int level)
	{
		int count = 0;
		string[] crimes = crimeCategories[level].crimes;
		foreach (string crime in crimes)
		{
			if (usedCrimes.Contains(crime))
			{
				++count;
			}
		}
		return count == crimes.Length;
	}

	// May need under some circumstances.
	// private string BreakString(string str)
	// {
		// string result = "";
		// for (int i = 0; i < str.Length; ++i)
		// {
			// result += str[i];
			// if (i % receiptCharacterLengthMax == 0)
			// {
				// result += '\n';
			// }
		// }
		// return result;
	// }

	[ContextMenu("Fuzz Unique Crime Generation")]
	private void FuzzUniqueCrimeGeneration()
	{
		usedCrimes.Clear();
		int count1 = 12, count2 = 3;
		for (int i = 0; i < count1; ++i)
		{
			KeyValuePair<int, string> minorPair = GetUniqueCrime();
			int minorLevel = minorPair.Key;
			string minorCrime = minorPair.Value;
			Debug.Log($"{minorLevel} {minorCrime}");
		}

		Debug.Log("BOSS");

		for (int i = 0; i < count2; ++i)
		{
			KeyValuePair<int, string> minorPair = GetUniqueCrime();
			int minorLevel = minorPair.Key;
			string minorCrime = minorPair.Value;

			KeyValuePair<int, string> majorPair1 = GetUniqueCrime(excludeLevel: minorLevel);
			int majorLevel = majorPair1.Key;
			string majorCrime1 = majorPair1.Value;
			KeyValuePair<int, string> majorPair2 = GetUniqueCrime(forceLevel: majorLevel, excludeCrime: majorCrime1);
			string majorCrime2 = majorPair2.Value;

			Debug.Log($"{minorLevel} {minorCrime} {majorLevel} {majorCrime1} {majorCrime2}");
		}
	}

	[System.Serializable] private struct CrimeCategory
	{
		public int level;
		public string[] crimes;
	}
}
