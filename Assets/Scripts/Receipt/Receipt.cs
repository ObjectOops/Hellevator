using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipt : MonoBehaviour
{
	[SerializeField] private int trustGain, trustLose;

	private ReceiptAnimator receiptAnimator;

	[HideInInspector] public string finePrint;
	[HideInInspector] public int level;

    private void Awake()
    {
		receiptAnimator = GetComponent<ReceiptAnimator>();
    }

    private void Start()
	{
		// Intentionally empty.
	}

	public void PrintSequence()
	{
		receiptAnimator.Print(finePrint);
	}

	public IEnumerator Judge(int buttonLevel)
	{
		receiptAnimator.Discard();
		bool correct = buttonLevel == level;
		Debug.Log("Button vs. Level: " + buttonLevel + ' ' + level, this);
        yield return SpiritManager.instance.activeSpirit.JudgementSequence(correct);
        yield return SpiritManager.instance.activeSpirit.DepartureSequence(); // Note here!

		GameManager.instance.SetTrust(GameManager.instance.trust + (correct ? trustGain : trustLose));
        yield return GameManager.instance.NextSpirit();
    }
}
