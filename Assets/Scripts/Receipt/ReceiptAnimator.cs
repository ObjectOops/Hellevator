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
			yield return new WaitForSeconds(textPrintDelay);
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
