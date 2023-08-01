using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipt : MonoBehaviour
{
	ReceiptAnimator receiptAnimator;

	public string crimes;
	public int level;

	private void Start()
	{
		receiptAnimator = GetComponent<ReceiptAnimator>();
	}

	public void PrintSequence()
    {

    }

	public void Judge(string buttonNameRaw)
    {

    }
}
