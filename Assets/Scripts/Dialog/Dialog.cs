using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] public static Dialog spiritBox, playerBox;

    private DialogAnimator dialogAnimator;

    private void Start()
    {
        dialogAnimator = GetComponent<DialogAnimator>();
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
