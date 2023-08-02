using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnimator : MonoBehaviour
{
    public static ElevatorAnimator instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Open")]
    public void ElevatorOpen()
    {
        Debug.Log("Yes");
        GetComponent<Animator>().SetBool("ElevatorOpen", true);
    }

    [ContextMenu("Unpress")]
    public void ElevatorClose()
    {
        GetComponent<Animator>().SetBool("ElevatorOpen", false);
    }
}
