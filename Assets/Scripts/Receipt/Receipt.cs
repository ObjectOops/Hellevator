using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipt : MonoBehaviour
{
	[SerializeField] private int trustGain, trustLose, resetTrust = 250; // May be overwritten in the inspector.
	[SerializeField] private float limboReduction;

	private ReceiptAnimator receiptAnimator;
	private int stage = 0;

	[HideInInspector] public string finePrint;
	[HideInInspector] public int level;

	private void Awake()
	{
		receiptAnimator = GetComponent<ReceiptAnimator>();
	}

	public void PrintSequence()
	{
		AudioManager.instance.PlaySFX("Receipt Unravel");
		receiptAnimator.Print(finePrint);
	}

	public IEnumerator Judge(int buttonLevel)
	{
		bool boss = GameManager.instance.boss, correct = buttonLevel == level;

		AudioManager.instance.PlaySFX(correct ? "Correct" : "Incorrect");

		if (boss && !correct)
		{
			string realName = SpiritManager.instance.activeSpirit.realName;
			string[] spiritDialog = SpiritManager.instance.activeSpirit.dialog;
			AudioClip[] spiritVoiceover = SpiritManager.instance.activeSpirit.voiceover;
			switch (stage)
			{
				// Spirit sequences inline with receipt for less back and forth.
				case 0:
					yield return Dialog.spiritBox.Speak($"\n{realName}\n\n{spiritDialog[4]}", null/*spiritVoiceover[4]*/);
					yield return new WaitForSeconds(1);
					Dialog.spiritBox.End();
					Button.ResetButtons();
					Button.levelSelected = false;
					break;
				default:
					yield return Dialog.spiritBox.Speak($"\n{realName}\n\n{spiritDialog[5]}", null/*spiritVoiceover[5]*/);
					GameManager.instance.judged = 0;
					receiptAnimator.Discard();
					yield return SpiritManager.instance.activeSpirit.DepartureSequence(buttonLevel);
					Dialog.spiritBox.End();
					GameManager.instance.SetTrust(resetTrust);
					yield return GameManager.instance.NextSpirit();
					break;
			}
			SetTrustInline(buttonLevel, correct);
			++stage;
			yield break;
		}
		yield return SpiritManager.instance.activeSpirit.JudgementSequence(correct);
		receiptAnimator.Discard();
		yield return SpiritManager.instance.activeSpirit.DepartureSequence(buttonLevel);

		SetTrustInline(buttonLevel, correct);

		string[] mephiDialogSuccess = SpiritManager.instance.mephiDialogSuccess;
		string[] mephiDialogFail = SpiritManager.instance.mephiDialogFail;
		AudioClip[] mephiVoiceoverSuccess = SpiritManager.instance.mephiVoiceoverSuccess;
		AudioClip[] mephiVoiceoverFail = SpiritManager.instance.mephiVoiceoverFail;
		int mephiRandom = Random.Range(0, Mathf.Min(mephiDialogSuccess.Length, mephiDialogFail.Length, mephiVoiceoverSuccess.Length, mephiVoiceoverFail.Length));
		yield return Dialog.mephiBox.Speak($"\n{GameManager.instance.mephiName}\n\n" + (correct ? 
			mephiDialogSuccess[mephiRandom] : 
			mephiDialogFail[mephiRandom]
		), correct ? 
			mephiVoiceoverSuccess[mephiRandom] : 
			mephiVoiceoverFail[mephiRandom]
		);
		Dialog.mephiBox.End();
		
		if (boss)
		{
			yield return GameManager.instance.NextDay();
		}
		yield return GameManager.instance.NextSpirit();

		void SetTrustInline(int buttonLevel, bool correct)
		{
			GameManager.instance.SetTrust(GameManager.instance.trust +
				(correct ?
					trustGain :
					buttonLevel == 1 ? (int)(trustGain * limboReduction) : trustLose
				)
			);
		}
	}
}
