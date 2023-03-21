using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    public float spreadSpeed;
    public float sizeX;
    float sizeY;
    float defaultSizeX;
    float defaultSizeY;

    public RectTransform rectTransform;
    CircleCollider2D coillder;
    ObjectPool objectPool;

    bool isCreated;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        coillder = GetComponent<CircleCollider2D>();
        objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();

        defaultSizeX = rectTransform.sizeDelta.x;
        defaultSizeY = rectTransform.sizeDelta.y;
        gameObject.SetActive(false);
        //StartCoroutine(SpreadPoison());
        //StartCoroutine(DestroyObject());
    }

    private void Update()
    {
        coillder.radius = sizeX / 2;
    }

    private void OnEnable()
    {
        if (isCreated)
        {

            StartCoroutine(SpreadPoison());
            StartCoroutine(DestroyObject());
        }
        isCreated = true;
    }

    IEnumerator SpreadPoison()
    {
        while (sizeX < 200)
        {
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
        sizeX = defaultSizeX;
        sizeY = defaultSizeY;
        rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
        objectPool.ReturnObject(this.gameObject, 2);
        //Destroy(this.gameObject);
    }
}
