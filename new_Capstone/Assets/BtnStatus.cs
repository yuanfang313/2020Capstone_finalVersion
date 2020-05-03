using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnStatus : MonoBehaviour
{

    public SpriteRenderer BtnRenderer;
    public Sprite Btn;
    public Sprite hoveredBtn;
    public AudioSource audioSource_HoverBtn;

    public GameObject tooltip;

    private Vector3 btnPosition;
    private bool btnHadTriggered = false;
    private float x;
    private float y;
    private float z;

    private void Start()
    {
        tooltip.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!btnHadTriggered)
        {
            btnHadTriggered = true;
            audioSource_HoverBtn.Play();
            btnPosition = transform.position;
            x = transform.position.x;
            y = transform.position.y + 0.35f;
            z = transform.position.z + 0.15f;
        }

        BtnRenderer.sprite = hoveredBtn;
        tooltip.SetActive(true);
        tooltip.transform.position = new Vector3(x, y, z);

    }

    private void OnTriggerExit(Collider ther)
    {
        BtnRenderer.sprite = Btn;
        tooltip.SetActive(false);
        btnHadTriggered = false;
    }
}
