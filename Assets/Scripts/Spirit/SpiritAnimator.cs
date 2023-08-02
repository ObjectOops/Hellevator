using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritAnimator : MonoBehaviour
{
    [SerializeField] private float lerpSpeed, lerpDelay;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator MoveTo(Transform posScale)
    {
        Vector2 originalPosition = transform.position;
        Vector2 originalScale = transform.localScale;
        for (float t = 0f; t < 1f; t += lerpSpeed * Time.deltaTime)
        {
            transform.position = Vector2.Lerp(originalPosition, posScale.position, t);
            transform.localScale = Vector2.Lerp(originalScale, posScale.localScale, t);
            yield return new WaitForSeconds(lerpDelay);
        }
    }

    public IEnumerator FadeIn()
    {
        // Alpha value 0 --> transparent.
        Color transparent = new(255, 255, 255, 0);
        Color opaque = new(255, 255, 255, 255);
        for (float t = 0f; t < 1f; t += lerpSpeed * Time.deltaTime)
        {
            Debug.Log(t);
            spriteRenderer.color = Color.Lerp(transparent, opaque, t);
            yield return new WaitForSeconds(lerpDelay);
        }
    }

    public IEnumerator FadeOut()
    {
        // Alpha value 0 --> transparent.
        Color transparent = new(255, 255, 255, 255);
        Color opaque = new(255, 255, 255, 0);
        for (float t = 0f; t < 1f; t += lerpSpeed * Time.deltaTime)
        {
            spriteRenderer.color = Color.Lerp(transparent, opaque, t);
            yield return new WaitForSeconds(lerpDelay);
        }
    }
}
