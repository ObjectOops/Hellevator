using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipt : MonoBehaviour
{
	[SerializeField] private string buttonNameStart;
	[SerializeField] private int trustGain, trustLose;

	private ReceiptAnimator receiptAnimator;

	[HideInInspector] public string finePrint;
	[HideInInspector] public int level;

	private void Start()
	{
		receiptAnimator = GetComponent<ReceiptAnimator>();
	}

	public void PrintSequence()
	{
		receiptAnimator.Print(finePrint);
	}

	public void Judge(Button button)
	{
		receiptAnimator.Discard();
		bool correct = $"{buttonNameStart}_{level}" == button.name;
		// SpiritManager.instance.activeSpirit.JudgementSequence(correct);
		// SpiritManager.instance.activeSpirit.DepartureSequence();
		// GameManager.instance.SetTrust(GameManager.instance.trust + (correct ? trustGain : trustLose))
		// GameManager.instance.NextSpirit();
	}
}
