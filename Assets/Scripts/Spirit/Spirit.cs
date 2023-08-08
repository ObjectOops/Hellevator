using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
	[SerializeField] private float greetingSequenceEndDelay, judgementSequenceEndDelay, departureSequenceEndDelay;

	[HideInInspector] public string realName, description, demise;
	[HideInInspector] public string[] dialog;

	private SpiritAnimator spiritAnimator;

	private void Awake()
	{
		spiritAnimator = GetComponent<SpiritAnimator>();
	}

	/*
	 * IMPORTANT
	 * The IEnumerator must be passed down the hierarchy all the way to Judge in the active Receipt or GameManager's Start!
	 */

	public IEnumerator GreetingSequence()
	{
		yield return ElevatorAnimator.instance.Shake();
		yield return ElevatorAnimator.instance.Open(0);
		spiritAnimator.FloatSpawn();
		spiritAnimator.Idle();
		yield return spiritAnimator.FadeIn();
		spiritAnimator.FloatStop();
		yield return spiritAnimator.MoveTo(SpiritManager.instance.movementPoints[1]);
		spiritAnimator.FloatMovedIn();
		yield return ElevatorAnimator.instance.Close(0);
		spiritAnimator.ExpressionReset();
		spiritAnimator.Talk();
		yield return Dialog.spiritBox.Speak($"\n{realName}\n\n{dialog[0]}"); // Spirit speaks.
		spiritAnimator.ExpressionReset();
		spiritAnimator.Idle();
		yield return Dialog.playerBox.Speak($"\n{GameManager.instance.playerName}\n\n{dialog[1]}"); // Player responds.

		yield return new WaitForSeconds(greetingSequenceEndDelay);
		Dialog.spiritBox.End();
		Dialog.playerBox.End();
	}

	public IEnumerator JudgementSequence(bool correct)
	{
		spiritAnimator.ExpressionReset();
		spiritAnimator.Talk();
		if (correct)
		{
			yield return Dialog.spiritBox.Speak($"\n{realName}\n\n{dialog[2]}");
			spiritAnimator.ExpressionReset();
			spiritAnimator.Correct();
		}
		else
		{
			yield return Dialog.spiritBox.Speak($"\n{realName}\n\n{dialog[3]}");
			spiritAnimator.ExpressionReset();
			spiritAnimator.Incorrect();
		}

		yield return new WaitForSeconds(judgementSequenceEndDelay);
	}

	public IEnumerator DepartureSequence(int level)
	{
		Dialog.spiritBox.End();

		yield return ElevatorAnimator.instance.Shake();
		yield return ElevatorAnimator.instance.Open(level);
		spiritAnimator.TerminateExpressionOverride();
		spiritAnimator.Idle();
		spiritAnimator.FloatStop();
		yield return spiritAnimator.MoveTo(SpiritManager.instance.movementPoints[2]);
		spiritAnimator.FloatSpawn();
		yield return spiritAnimator.FadeOut();
		yield return ElevatorAnimator.instance.Close(level);
		Destroy(gameObject);

		yield return new WaitForSeconds(departureSequenceEndDelay);
	}
}
