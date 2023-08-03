using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritAnimator : MonoBehaviour
{
    [SerializeField] private float moveLerpSpeed, fadeLerpSpeed, lerpDelay;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator MoveTo(Transform posScale)
    {
        Debug.Log("Moving To: " + posScale.position + ' ' + posScale.localScale, this);
        Vector2 originalPosition = transform.position;
        Vector2 originalScale = transform.localScale;
        Debug.Log("Original Loc.: " + originalPosition + ' ' + originalScale);
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
    }

    public IEnumerator FadeOut()
    {
        // Alpha value 0 --> transparent.
        Color transparent = new(255, 255, 255, 1);
        Color opaque = new(255, 255, 255, 0);
        for (float t = 0f; t <= 1f; t += fadeLerpSpeed * Time.deltaTime)
        {
            spriteRenderer.color = Color.Lerp(transparent, opaque, t);
            yield return new WaitForSeconds(lerpDelay);
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
}
