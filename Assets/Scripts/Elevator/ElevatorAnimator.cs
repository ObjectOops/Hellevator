using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnimator : MonoBehaviour
{
	public static ElevatorAnimator instance;
	public static bool opened;

	[SerializeField] private new Camera camera;
	[SerializeField] private float shortDelay, cameraShake, cameraShakeDelay;
	[SerializeField] private int cameraShakeIterations;

	private Animator animator;

	private void Awake()
	{
		instance = this;
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		// Intentionally empty.
	}

	public IEnumerator Open()
    {
		animator.SetTrigger("open");
		while (!opened)
        {
			yield return new WaitForSeconds(shortDelay);
        }
    }

	public IEnumerator Close()
	{
		animator.SetTrigger("close");
		while (opened)
		{
			yield return new WaitForSeconds(shortDelay);
		}
	}

	public IEnumerator Shake()
    {
		Vector2 cameraPosOriginal = camera.transform.position;
		for (int i = 0; i < cameraShakeIterations; ++i)
        {
			camera.transform.position = cameraPosOriginal + 
				new Vector2(Random.Range(0, cameraShake), Random.Range(0, cameraShake));
			yield return new WaitForSeconds(cameraShakeDelay);
        }
		camera.transform.position = cameraPosOriginal;
    }

	public void Closed()
    {
		opened = false;
    }

	public void Opened()
    {
		opened = true;
    }
}
