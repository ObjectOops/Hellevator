using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public static Dialog spiritBox, playerBox;

    [SerializeField] private bool spirit;

    private DialogAnimator dialogAnimator;

    private void Awake()
    {
        dialogAnimator = GetComponent<DialogAnimator>();
        if (spirit)
        {
            spiritBox = this;
        }
        else
        {
            playerBox = this;
        }
        spiritBox.gameObject.SetActive(false);
        playerBox.gameObject.SetActive(false);
    }

    private void Start()
    {
        // Intentionally empty.
    }

    public IEnumerator Speak(string speech)
    {
        gameObject.SetActive(true);
        yield return dialogAnimator.LinearIn(speech);
    }

    public static void EndAll()
    {
        spiritBox.gameObject.SetActive(false);
        playerBox.gameObject.SetActive(false);
    }

    [ContextMenu("Test Speak")]
    public void TestSpeak()
    {
        StartCoroutine(Speak("Test."));
    }

    [ContextMenu("Test End All")]
    public void TestEndAll()
    {
        EndAll();
    }
}
