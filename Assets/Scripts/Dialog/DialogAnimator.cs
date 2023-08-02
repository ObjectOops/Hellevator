using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogAnimator : MonoBehaviour
{
	[SerializeField] private float textDelay;

	private Animator animator;
	private TextMeshPro textMesh;

	private void Start()
	{
		animator = GetComponent<Animator>();
		textMesh = GetComponent<TextMeshPro>();
	}

	public IEnumerator LinearIn(string text)
    {
		textMesh.text = "";
		foreach (char c in text)
		{
			textMesh.text += c;
			yield return new WaitForSeconds(textDelay);
		}
	}
}
