using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnimator : MonoBehaviour
{
	public static ElevatorAnimator instance;

	[SerializeField] private Animator cameraAnimator;
	[SerializeField] private float /*waitDelay, */elevatorTransitDelay;
	[SerializeField] private GameObject[] floors;

	private Animator animator;
	private bool opened = false;

	private void Awake()
	{
		instance = this;
		animator = GetComponent<Animator>();
		if (floors.Length != 10)
		{
			Debug.LogWarning("The number of floors must be ten - one for the lobby and the rest for each of the nine levels.", this);
		}
		foreach (GameObject floor in floors)
		{
			floor.SetActive(false);
		}
		floors[0].SetActive(true);
	}

	public IEnumerator Open(int level)
	{
		floors[level].SetActive(true);
		animator.SetTrigger("open");
		// while (!opened)
		// {
		// yield return new WaitForSeconds(waitDelay);
		// }
		float timeout = 0;
		yield return new WaitWhile(() => !opened || (timeout += Time.deltaTime) > 5f);
	}

	public IEnumerator Close(int level)
	{
		animator.SetTrigger("close");
		// while (opened)
		// {
		// yield return new WaitForSeconds(waitDelay);
		// }
		float timeout = 0;
		yield return new WaitWhile(() => opened || (timeout += Time.deltaTime) > 5f);
		floors[level].SetActive(false);
	}

	public IEnumerator Shake()
	{
		yield return new WaitForSeconds(elevatorTransitDelay);
		cameraAnimator.SetTrigger("shake");
		cameraAnimator.SetTrigger("stop");
		yield return new WaitForSeconds(elevatorTransitDelay);
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
