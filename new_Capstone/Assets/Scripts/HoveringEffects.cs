using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringEffects : MonoBehaviour
{ 
    public GameObject portalParticle;
    public Sprite sprite_Original;
    public Sprite sprite_Hovered;
    public bool hadHit = false;

    private SpriteRenderer BtnSpriteRenderer;
    private Transform originalTransform;
    private AudioSource audioSource_hover;
    private Vector3 originalScale;
   
    private void Start()
    {
        portalParticle.SetActive(false);

        originalTransform = transform;
        originalScale = transform.localScale;
        BtnSpriteRenderer = GetComponent<SpriteRenderer>();
        audioSource_hover = GetComponent<AudioSource>();
    }
 
    private void OnTriggerEnter(Collider other)
    {

        BtnSpriteRenderer.sprite = sprite_Hovered;

        if (!hadHit)
        {
            transform.localScale = transform.localScale * 1.1f;
            portalParticle.SetActive(true);
            audioSource_hover.Play();
            hadHit = true;
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        BtnSpriteRenderer.sprite = sprite_Original;
        portalParticle.SetActive(false);
        transform.localScale = originalScale;
        hadHit = false;  
    }

    
}
