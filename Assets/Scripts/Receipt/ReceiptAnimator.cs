using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReceiptAnimator : MonoBehaviour
{
	[SerializeField] private float textPrintDelay;

	private Animator animator;
	private TextMeshPro textMesh;

	private bool isPrinting;

    private void Awake()
    {
		animator = GetComponent<Animator>();
		textMesh = transform.GetComponentInChildren<TextMeshPro>();        
    }

    private void Start()
	{
		// Intentionally empty.
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
			}
			yield return new WaitForSeconds(textPrintDelay);
		}
	}

	public void Trash()
	{
		Destroy(gameObject);
	}

	[ContextMenu("Test Print Animation")]
	public void TestPrint()
	{
		Print(textMesh.text);
	}

	[ContextMenu("Test Discard Animation")]
	public void TestDiscard()
	{
		Discard();
	}
}
