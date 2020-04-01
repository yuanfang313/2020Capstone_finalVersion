using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringEffects2 : MonoBehaviour
{
    public AudioSource audioSource_hover2;
    private Vector3 changedScale;
    private Vector3 originalScale;
    private bool hadHit = false;
    private void Start()
    {
        changedScale = new Vector3(0.3f, 0.3f, 0.3f);
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.localScale = changedScale;
        if (!hadHit)
        {
            audioSource_hover2.Play();
            hadHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.localScale = originalScale;
        hadHit = false;
    }
}
