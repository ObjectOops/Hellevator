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

	public IEnumerator Judge(Button button)
	{
		receiptAnimator.Discard();
		bool correct = $"{buttonNameStart}_{level}" == button.name;
        yield return SpiritManager.instance.activeSpirit.JudgementSequence(correct);
        yield return SpiritManager.instance.activeSpirit.DepartureSequence();

		GameManager.instance.SetTrust(GameManager.instance.trust + (correct ? trustGain : trustLose));
        yield return GameManager.instance.NextSpirit();
    }
}
