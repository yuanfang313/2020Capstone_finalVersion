using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnStatus : MonoBehaviour
{
    public SpriteRenderer BtnRenderer;
    public Sprite Btn;
    public Sprite hoveredBtn;
    public Text score1;

    public GameObject tooltip;
    
    private float y;
    private float y1;

    
    private void Start()
    {
        tooltip.SetActive(false);

        y = transform.position.y;
        y1 = y + 0.2f;
    }
    private void OnTriggerEnter(Collider other)
    {
        BtnRenderer.sprite = hoveredBtn;
        tooltip.SetActive(true);

        //transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        //transform.position = new Vector3(transform.position.x, y1, transform.position.z);
    }

    private void OnTriggerExit(Collider ther)
    {
        BtnRenderer.sprite = Btn;
        tooltip.SetActive(false);
    }
}
