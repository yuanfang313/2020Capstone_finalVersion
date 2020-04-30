using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringEffects : MonoBehaviour
{
    public AudioSource audioSource_hover;
    public GameObject portalParticle;
    public Transform originalTransform;
    private Vector3 changedScale;
    private Vector3 originalScale;
    private float maxTimer = 1.5f;
    private float _tempTimer = 0f;
    private bool direction;

    private bool hadHit = false;

    private void Start()
    {
        if(this.name == "Tutorial")
        {
            direction = false;
        } else
        {
            direction = true;
        }

        portalParticle.SetActive(false);
        changedScale = new Vector3 (0.6f, 0.6f, 0.6f);
        originalScale = transform.localScale;
    }
    private void Update()
    {
        MoveUpAndDown();
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.localScale = changedScale;
        if (!hadHit)
        {
            portalParticle.SetActive(true);
            audioSource_hover.Play();
            hadHit = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        portalParticle.SetActive(false);
        transform.localScale = originalScale;
        hadHit = false;  
    }

    private void MoveUpAndDown()
    {
        float speedScale = Mathf.Pow(Mathf.Abs(_tempTimer - maxTimer / 2f), 0.5f) * 0.05f + 0.1f;

        _tempTimer += Time.deltaTime;
        if (_tempTimer > maxTimer)
        {
            _tempTimer = 0f;
            direction = !direction;
        }
        if (!hadHit)
        {
            if (direction)
            {
                transform.position = transform.position + Vector3.up * transform.localScale.x * Time.deltaTime * 0.015f/speedScale;
            }
            else
            {
                transform.position = transform.position + Vector3.up * transform.localScale.x * Time.deltaTime * -0.015f/speedScale;
            }
        }
  
    }
}
