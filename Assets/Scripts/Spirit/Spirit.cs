using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    private SpiritManager spiritManager;
    private ElevatorAnimator elevatorAnimator;

    [HideInInspector]
    public enum phases { Greeting, Judgement, Departure };
    phases phase;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spiritManager == null)
        {
            spiritManager = SpiritManager.instance;
        }
        if(elevatorAnimator == null)
        {
            elevatorAnimator = ElevatorAnimator.instance;
            elevatorAnimator.ElevatorOpen();
        }
        switch (phase)
        {
            //Fading in
            case phases.Greeting:
                if(transform.localScale.y < 0.2)
                {
                    transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime / 20f, transform.localScale.y + Time.deltaTime / 20f, transform.localScale.z);
                }
                else
                {
                    elevatorAnimator.ElevatorClose();
                }
                break;
            //Moving to the side and waiting for the elevator
            case phases.Judgement:
                if(transform.position.x < spiritManager.pos2.position.x)
                {
                    transform.position = new Vector3(transform.position.x +  Time.deltaTime / 3, transform.position.y, transform.position.z);
                }
                break;
            case phases.Departure:
                break;
        }
    }

}
