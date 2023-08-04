using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    [SerializeField] private List<string> names;
    [SerializeField] [Scene] private List<string> scenes;

    private Dictionary<string, string> map = new();

    private void Start()
    {
        instance = this;
        if (names.Count != scenes.Count)
        {
            Debug.LogWarning("The number of names does not match the numbers of scenes.", this);
        }
        int n = Mathf.Max(names.Count, scenes.Count);
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
