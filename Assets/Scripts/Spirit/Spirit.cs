using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
	[SerializeField] private ElevatorAnimator elevatorAnimator;
    [SerializeField] private float greetingSequenceEndDelay;
	
	[HideInInspector] public string realName, description, demise;
	[HideInInspector] public List<string> dialog;

	private SpiritAnimator spiritAnimator;

    private void Start()
    {
        spiritAnimator = GetComponent<SpiritAnimator>();
    }

    public IEnumerator GreetingSequence()
    {
/*        yield return spiritAnimator.FadeIn(); // Spirit fades in.
        yield return spiritAnimator.MoveTo(SpiritManager.instance.movementPoints[1]); // Spirit moves in.
        yield return Dialog.spiritBox.Print(dialog[0]); // Spirit speaks.
        yield return Dialog.playerBox.Print(dialog[1]); // Player responds.
*/        yield return new WaitForSeconds(greetingSequenceEndDelay);
    }


}
