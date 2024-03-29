using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PressToStart : MonoBehaviour
{
	private static bool booted = false;

	[SerializeField] private VideoPlayer cutscene;
	[SerializeField] private GameObject[] otherObjects;
	[SerializeField] private GameObject loading, goose;

	private SpriteRenderer backgroundRenderer;

	private void Awake()
	{
		backgroundRenderer = GetComponent<SpriteRenderer>();
		loading.SetActive(false);
		goose.SetActive(true);
	}

	private void OnMouseDown()
	{
		if (!booted && !UIManager.instance.paused)
		{
			AudioManager.instance.StopMusic();
			backgroundRenderer.enabled = false;
			foreach (GameObject obj in otherObjects)
			{
				obj.SetActive(false);
			}
			booted = true;
			StartCoroutine(StartCutscene());
		}
		else if (!UIManager.instance.paused)
		{
			loading.SetActive(true);
			SceneManager.instance.Load("Game");
		}
	}

	private IEnumerator StartCutscene()
	{
		// The wait after cutscene start is necessary.
		yield return new WaitForSeconds(1f);
		cutscene.Play();
		yield return new WaitForSeconds(1f);
		float timeout = 0;
		yield return new WaitWhile(() => cutscene.isPlaying && (timeout += Time.deltaTime) < 40f);
		yield return new WaitForSeconds(1f);
		loading.SetActive(true);
		SceneManager.instance.Load("Game");
	}
}
