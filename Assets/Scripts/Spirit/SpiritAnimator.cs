using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritAnimator : MonoBehaviour
{
	[SerializeField] private float moveLerpSpeed, fadeLerpSpeed, lerpDelay;
	
	[HideInInspector] public AnimationClip talkingAnimation;

	private Animator animator;
	private new Animation animation;
	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		animation = GetComponent<Animation>();
		spriteRenderer = GetComponent<SpriteRenderer>();
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

	[ContextMenu("Talk Start")]
	public void TalkStart()
	{
		animation.clip = talkingAnimation;
		animation.Play("Pirate_Talk");
	}

	[ContextMenu("Talk Stop")]
	public void TalkStop()
	{
		animation.Stop();
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
}
