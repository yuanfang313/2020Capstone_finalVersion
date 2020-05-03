using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    private float maxTimer = 1.0f;
    private float _tempTimer = 0f;
    private bool direction = false;

    void Update()
    {
        float speedScale = Mathf.Pow(Mathf.Abs(_tempTimer - maxTimer / 2f), 0.5f) * 0.4f + 0.1f;

        _tempTimer += Time.deltaTime;

        if (_tempTimer > maxTimer)
        {
            _tempTimer = 0f;
            direction = !direction;
        }

        if (direction)
            transform.position = transform.position + Vector3.up * transform.localScale.x * Time.deltaTime * 0.015f / speedScale;
        else
            transform.position = transform.position + Vector3.up * transform.localScale.x * Time.deltaTime * -0.015f / speedScale;
    }
}
