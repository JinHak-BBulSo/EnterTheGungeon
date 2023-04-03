using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    public string enemyName;
    public float spreadSpeed;
    public float sizeX;
    float sizeY;
    float defaultSizeX;
    float defaultSizeY;

    public RectTransform rectTransform;
    ObjectPool objectPool;

    bool isCreated;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();

        //defaultSizeX = rectTransform.sizeDelta.x;
        defaultSizeX = rectTransform.localScale.x;
        //defaultSizeY = rectTransform.sizeDelta.y;
        defaultSizeY = rectTransform.localScale.y;
        gameObject.SetActive(false);
        //StartCoroutine(SpreadPoison());
        //StartCoroutine(DestroyObject());
    }

    private void Update()
    {
    }

    private void OnEnable()
    {
        if (isCreated)
        {

            StartCoroutine(SpreadPoison());
            StartCoroutine(ReturnObject());
        }
        isCreated = true;
    }

    IEnumerator SpreadPoison()
    {
        while (sizeX < 300)
        {
            //sizeX = rectTransform.sizeDelta.x;
            sizeX = rectTransform.localScale.x;
            //sizeY = rectTransform.sizeDelta.y;
            sizeY = rectTransform.localScale.y;
            sizeX += spreadSpeed * Time.deltaTime;
            sizeY += spreadSpeed * Time.deltaTime;
            //rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.localScale = new Vector2(sizeX, sizeY);
            yield return null;
        }
    }

    IEnumerator ReturnObject()
    {
        yield return new WaitForSeconds(5);
        enemyName = default;
        sizeX = defaultSizeX;
        sizeY = defaultSizeY;
        //rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
        rectTransform.localScale = new Vector2(sizeX, sizeY);
        StopAllCoroutines();
        objectPool.ReturnObject(this.gameObject, 2);
        //Destroy(this.gameObject);
    }
}
