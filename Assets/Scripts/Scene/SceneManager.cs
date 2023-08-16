using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
	public static SceneManager instance;

	[SerializeField] private string[] names;
	[SerializeField] [Scene] private string[] scenes;

	private readonly Dictionary<string, string> map = new();

	private void Awake()
	{
		instance = this;
		if (names.Length != scenes.Length)
		{
			Debug.LogWarning("The number of names does not match the numbers of scenes.", this);
		}
		int n = Mathf.Max(names.Length, scenes.Length);
		for (int i = 0; i < n; ++i)
		{
			map.Add(names[i], scenes[i]);
		}
	}

	public void Load(string shortName)
	{
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(map[shortName]);
	}
}
