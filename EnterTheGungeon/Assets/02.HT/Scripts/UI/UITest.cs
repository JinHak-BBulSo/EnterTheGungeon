using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    Camera mainCamera;
    public bool isTest;
    SpriteRenderer spriteRenderer;
    float grayAmount = 0;
    bool isGrayScaleChange;
    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        Vector3 screenPoint_ = mainCamera.WorldToViewportPoint(this.transform.position);
        if (screenPoint_.x >= 0 && screenPoint_.x <= 1 && screenPoint_.y >= 0 && screenPoint_.y <= 1)
        {
            if (!isGrayScaleChange)
            {
                StartCoroutine(ChangeGrayScale());
            }
        }
    }

    IEnumerator ChangeGrayScale()
    {
        if (grayAmount <= 1)
        {
            grayAmount += 0.5f * Time.deltaTime;
            spriteRenderer.material.SetFloat("_GrayscaleAmount", grayAmount);
            yield return null;
        }
        else
        {
            isGrayScaleChange = true;
        }

    }
}
