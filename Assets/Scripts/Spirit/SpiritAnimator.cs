using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritAnimator : MonoBehaviour
{
	[SerializeField] private float moveLerpSpeed, fadeLerpSpeed, lerpDelay;

	[HideInInspector] public AnimationClip idleAnimation, talkAnimation;
	[HideInInspector] public Sprite spriteCorrect, spriteIncorrect;

	private Animator animator;
	private AnimatorOverrideController overrideController;
	private SpriteRenderer spriteRenderer;
	private EAO expressionAnimationOverride = EAO.NONE;

	// Very important, easy to mix up, constants.
	private const string idleAnimationDiscriminator = "Spirit_Idle";
	private const string talkAnimationDiscriminator = "Spirit_Talk";

	private void Awake()
	{
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

		animator.runtimeAnimatorController = overrideController;
	}

	public IEnumerator MoveTo(Transform posScale)
	{
		Vector2 originalPosition = transform.position;
		Vector2 originalScale = transform.localScale;
		for (float t = 0f; t < 1f; t += moveLerpSpeed * Time.deltaTime)
		{
			transform.position = Vector2.Lerp(originalPosition, posScale.position, t);
			transform.localScale = Vector2.Lerp(originalScale, posScale.localScale, t);
			yield return new WaitForSeconds(lerpDelay);
		}
	}

	public IEnumerator FadeIn()
	{
		// Alpha value 0 --> transparent, 1 --> opaque.
		Color transparent = new(255, 255, 255, 0);
		Color opaque = new(255, 255, 255, 1);
		for (float t = 0f; t <= 1f; t += fadeLerpSpeed * Time.deltaTime)
		{
			spriteRenderer.color = Color.Lerp(transparent, opaque, t);
			yield return new WaitForSeconds(lerpDelay);
		}
		spriteRenderer.color = opaque;
	}

	public IEnumerator FadeOut()
	{
		// Alpha value 0 --> transparent.
		Color opaque = new(255, 255, 255, 1);
		Color transparent = new(255, 255, 255, 0);
		for (float t = 0f; t <= 1f; t += fadeLerpSpeed * Time.deltaTime)
		{
			spriteRenderer.color = Color.Lerp(opaque, transparent, t);
			yield return new WaitForSeconds(lerpDelay);
		}
		spriteRenderer.color = transparent;
	}

	public void FloatSpawn()
	{
		animator.SetTrigger("float_spawn");
	}

	public void FloatMovedIn()
	{
		animator.SetTrigger("float_moved_in");
	}

	public void FloatStop()
	{
		animator.SetTrigger("stop");
	}

	/*
	 * Warning:
	 * The following two functions are every similar and are extremely easy to mix up.
	 * If the spirits do not appear, it may be due to a malconfiguration or misuse of these functions.
	 * Also see the feature animation layer.
	 */

	public void Idle()
	{
		overrideController[idleAnimationDiscriminator] = idleAnimation;
		animator.SetTrigger("idle");
	}

	public void Talk()
	{
		overrideController[talkAnimationDiscriminator] = talkAnimation;
		animator.SetTrigger("talk");
	}

	public void ExpressionReset()
	{
		animator.SetTrigger("reset");
	}

	public void Correct()
	{
		expressionAnimationOverride = EAO.CORRECT;
	}

	public void Incorrect()
	{
		expressionAnimationOverride = EAO.INCORRECT;
	}

	public void TerminateExpressionOverride()
	{
		expressionAnimationOverride = EAO.NONE;
	}

	// The only update function. This one is absolutely necessary.
	private void LateUpdate()
	{
		switch (expressionAnimationOverride)
		{
			case EAO.CORRECT:
				spriteRenderer.sprite = spriteCorrect;
				break;
			case EAO.INCORRECT:
				spriteRenderer.sprite = spriteIncorrect;
				break;
		}
	}

	[ContextMenu("Test Fade In")]
	private void TestFadeIn()
	{
		StartCoroutine(FadeIn());
	}

	[ContextMenu("Test Fade Out")]
	private void TestFadeOut()
	{
		StartCoroutine(FadeOut());
	}

	private enum EAO
	{
		NONE, CORRECT, INCORRECT
	}
}
