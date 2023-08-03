using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToStart : MonoBehaviour
{
    public string sceneName;
    public void OnMouseUp()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

}
