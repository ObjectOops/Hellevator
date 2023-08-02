using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogAnimator : MonoBehaviour
{
	[SerializeField] private float textDelay;

	private Animator animator;
	private TextMeshPro textMesh;

    private void Awake()
    {
		textMesh = GetComponentInChildren<TextMeshPro>();
		animator = GetComponent<Animator>();
		Debug.Log("DialogAnimator components initiated.", this);
	}

    private void Start()
	{
		// Intentionally empty.
	}

	public IEnumerator LinearIn(string text)
    {
		Debug.Log(textMesh == null, this);
		textMesh.text = "";
		foreach (char c in text)
		{
			textMesh.text += c;
			yield return new WaitForSeconds(textDelay);
		}
	}
}
