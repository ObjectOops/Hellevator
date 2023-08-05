using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAdjustButton : MonoBehaviour
{
    private static List<VolumeAdjustButton> buttons = new();

    [SerializeField] private string mixerGroup;
    [SerializeField] private float volumeLevel;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        buttons.Add(this);
    }

    private void OnMouseDown()
    {
        /*Debug.Log("Adjusting " + volumeLevel);
        AudioManager.instance.AdjustVolume(mixerGroup, volumeLevel);
        foreach (VolumeAdjustButton button in buttons)
        {
            if (button != this)
            {
                button.animator.ResetTrigger("press");
            }
        }
        animator.SetTrigger("press");*/
        animator.SetTrigger("press");
    }
}
