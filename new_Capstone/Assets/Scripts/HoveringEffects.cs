using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringEffects : MonoBehaviour
{
    public AudioSource audioSource_hover;
    private Vector3 changedScale;
    private Vector3 originalScale;

    private bool hadHit = false;

    private void Start()
    {
        changedScale = new Vector3 (0.7f, 0.7f, 0.7f);
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.localScale = changedScale;
        if (!hadHit)
        {
            audioSource_hover.Play();
            hadHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.localScale = originalScale;
        hadHit = false;
    }
}
