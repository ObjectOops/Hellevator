using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    [SerializeField] private float greetingSequenceEndDelay;
	
	[HideInInspector] public string realName, description, demise;
	[HideInInspector] public List<string> dialog;

	private SpiritAnimator spiritAnimator;

    private void Start()
    {
        spiritAnimator = GetComponent<SpiritAnimator>();
    }

    /*
     * IMPORTANT
     * The IEnumerator must be passed down the hierarchy all the way to Judge in the active Receipt or GameManager's Start!
     */

    public IEnumerator GreetingSequence()
    {
        Debug.Log("Performing GreetingSequence animations.", this);
        yield return ElevatorAnimator.instance.Open();
        Debug.Log("Elevator opening animation complete.", this);
        yield return spiritAnimator.FadeIn(); // Spirit fades in.
        Debug.Log("Spirit fade in animation complete.", this);
        yield return spiritAnimator.MoveTo(SpiritManager.instance.movementPoints[1]); // Spirit moves in.
        yield return ElevatorAnimator.instance.Close();
        Debug.Log("Speak 1", this);
        yield return Dialog.spiritBox.Speak(realName + "\n\n" + dialog[0]); // Spirit speaks.
        Debug.Log("Speak 2", this);
        yield return Dialog.playerBox.Speak("Dante\n\n" + dialog[1]); // Player responds.

        Debug.Log("Animations suceeded.", this);
        yield return new WaitForSeconds(greetingSequenceEndDelay);
        Dialog.EndAll();
    }

    public IEnumerator JudgementSequence(bool correct)
    {
        if (correct)
        {
            yield return Dialog.spiritBox.Speak(dialog[2]);
        }
        else
        {
            yield return Dialog.spiritBox.Speak(dialog[3]);
        }
        yield return spiritAnimator.MoveTo(SpiritManager.instance.movementPoints[2]);
        // yield return ElevatorAnimator.instance.Shake();
    }

    public IEnumerator DepartureSequence(int buttonLevel)
    {
        // yield return ElevatorAnimator.instance.Shake();
        yield return ElevatorAnimator.instance.Open();
        yield return spiritAnimator.MoveTo(SpiritManager.instance.movementPoints[3]);
        yield return spiritAnimator.FadeOut();
        yield return ElevatorAnimator.instance.Close();
        Color swap = ReceiptManager.instance.levelBackground[buttonLevel].color;
        swap.a = 0;
        ReceiptManager.instance.levelBackground[buttonLevel].color = swap;
        Destroy(gameObject);
    }
}
