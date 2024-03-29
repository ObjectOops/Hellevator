using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour
{
	public static SpiritManager instance;

	[SerializeField] private GameObject spiritPrefab;
	[SerializeField] private Trait[] traits;
	public Transform[] movementPoints;

	[Header("Additional Feature Parameters")]
	public string[] mephiDialogSuccess, mephiDialogFail, mephiIntermezzo;
	public AudioClip[] mephiVoiceoverSuccess, mephiVoiceoverFail, mephiVoiceoverIntermezzo;

	[HideInInspector] public Spirit activeSpirit;

	private void Awake()
	{
		instance = this;
	}

	public IEnumerator GenerateSpirit(int day, int index)
	{
		// `day` starts from 1.
		Trait trait = traits[(day - 1) * GameManager.instance.limit + index];
		GameObject spiritObject = Instantiate(spiritPrefab);

		Spirit spirit = spiritObject.GetComponent<Spirit>();
		SpiritAnimator spiritAnimator = spiritObject.GetComponent<SpiritAnimator>();

		TransferTransform(spirit, movementPoints[0]); // Move to spawn point.

		spiritAnimator.idleAnimation = trait.idleAnimation;
		spiritAnimator.talkAnimation = trait.talkAnimation;
		spiritAnimator.spriteCorrect = trait.spriteCorrect;
		spiritAnimator.spriteIncorrect = trait.spriteinCorrect;

		spirit.realName = trait.realName;
		spirit.description = trait.description;
		spirit.demise = trait.demise;
		spirit.dialog = trait.dialog;
		spirit.voiceover = trait.voiceover;
		activeSpirit = spirit;
		yield return spirit.GreetingSequence();
	}

	public void TransferTransform(Spirit s, Transform t)
	{
		s.transform.SetPositionAndRotation(t.position, t.rotation);
		s.transform.localScale = t.localScale;
	}

	[System.Serializable] private struct Trait
	{
		public AnimationClip idleAnimation, talkAnimation;
		public Sprite spriteCorrect, spriteinCorrect;
		public string realName, description, demise;
		public string[] dialog;
		public AudioClip[] voiceover;
	}
}
