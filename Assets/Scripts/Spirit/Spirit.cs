using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    private SpiritManager spiritManager;

    [HideInInspector]
    public int phase;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = spiritManager.pos1.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            //Fading in
            case 0:
                if(transform.localScale.x < 2)
                {
                    transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime, transform.localScale.y, transform.localScale.z + Time.deltaTime);
                }
                break;
            //Moving to the side and waiting for the elevator
            case 1:
                transform.position = Vector3.Lerp(spiritManager.pos1.position, spiritManager.pos2.position, 2);
                break;
            case 2:
                break;
        }
    }
}
