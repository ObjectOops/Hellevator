using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReceiptAnimator : MonoBehaviour
{
	[SerializeField] private float textPrintDelay, overflowFontSizeReduction;

	private Animator animator;
	private TextMeshPro textMesh;

	private bool isPrinting;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		textMesh = transform.GetComponentInChildren<TextMeshPro>();        
	}

	// Quickly added skip feature.
	private void OnMouseDown()
	{
		textPrintDelay = 0f;
	}

	public void Print(string text)
	{
		isPrinting = true;
		animator.SetTrigger("print");
		StartCoroutine(LinearIn(text));
	}

	public void Discard()
	{
		isPrinting = false;
		animator.SetTrigger("discard");
		textMesh.text = "";
	}

	private IEnumerator LinearIn(string text)
	{
		// Nesting.
		textMesh.text = "";
		foreach (char c in text)
		{
			if (isPrinting)
			{
				textMesh.text += c;
				if (textMesh.isTextOverflowing)
				{
					textMesh.fontSize -= overflowFontSizeReduction;
				}
			}
			if (textPrintDelay == 0f)
			{
				textMesh.text = text;
				StartCoroutine(Shrink());
				break;
			}
			yield return new WaitForSeconds(textPrintDelay);
		}
		StartCoroutine(GameManager.instance.DecisionTimer());
	}

	private IEnumerator Shrink()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		while (textMesh.isTextOverflowing)
		{
			textMesh.fontSize -= overflowFontSizeReduction;
			yield return null;
		}
	}

	public void Trash() // Called by animation event.
	{
		Destroy(gameObject);
	}

	[ContextMenu("Test Print Animation")]
	private void TestPrint()
	{
		Print(textMesh.text);
	}

	[ContextMenu("Test Discard Animation")]
	private void TestDiscard()
	{
		Discard();
	}
}
