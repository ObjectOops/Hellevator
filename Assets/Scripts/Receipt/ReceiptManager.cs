using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiptManager : MonoBehaviour
{
	public static ReceiptManager instance;

	[SerializeField] private GameObject receiptPrefab;
	[SerializeField] private List<Crime> crimes;
	[SerializeField] private GameObject receiptSpawn;

	public Receipt activeReceipt;

	private void Start()
	{
		instance = this;
	}

	public void GenerateReceipt()
    {

    }

	[System.Serializable] private struct Crime
	{
		public string crime;
		public int level;
	}
}
