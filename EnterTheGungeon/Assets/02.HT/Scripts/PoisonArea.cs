using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    public float spreadSpeed;
    float sizeX;
    float sizeY;

    RectTransform rectTransform;
    CircleCollider2D coillder;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        coillder = GetComponent<CircleCollider2D>();
        StartCoroutine(SpreadPoison());
        StartCoroutine(DestroyObject());
    }

    private void Update()
    {
        coillder.radius = sizeX/2;
    }

    IEnumerator SpreadPoison()
    {
        while (sizeX < 200)
        {
            //transform.localScale += Vector3.one * spreadSpeed * Time.deltaTime;
            sizeX = rectTransform.sizeDelta.x;
            sizeY = rectTransform.sizeDelta.y;
            sizeX += spreadSpeed * Time.deltaTime;
            sizeY += spreadSpeed * Time.deltaTime;
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            yield return null;
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
