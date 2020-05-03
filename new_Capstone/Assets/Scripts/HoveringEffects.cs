using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringEffects : MonoBehaviour
{
    public AudioSource audioSource_hover;
    public GameObject portalParticle;
    public Transform originalTransform;

    public SpriteRenderer BtnSpriteRenderer;
    public Sprite OriginalBtn;
    public Sprite HoveredBtn;

    private Vector3 changedScaleForPlay;
    private Vector3 changedScaleForTutorial;
    private Vector3 originalScale;
    private float maxTimer = 1.0f;
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
        originalScale = transform.localScale;
        changedScaleForPlay = new Vector3 (2.7f, 2.7f, 2.7f);
        changedScaleForTutorial = new Vector3 (1.9f, 1.9f, 1.9f); 
    }
    private void Update()
    {
        MoveUpAndDown();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.name == "Tutorial")
            transform.localScale = changedScaleForTutorial;
        else if(this.name == "Module_1")
            transform.localScale = changedScaleForPlay;

        BtnSpriteRenderer.sprite = HoveredBtn;

        if (!hadHit)
        {
            portalParticle.SetActive(true);
            audioSource_hover.Play();
            hadHit = true;
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        BtnSpriteRenderer.sprite = OriginalBtn;
        portalParticle.SetActive(false);
        transform.localScale = originalScale;
        hadHit = false;  
    }

    private void MoveUpAndDown()
    {
        float speedScale = Mathf.Pow(Mathf.Abs(_tempTimer - maxTimer / 2f), 0.5f) * 0.5f + 0.1f;

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
