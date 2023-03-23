using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGorgunSonicWave : MonoBehaviour
{
    RectTransform rectTransform;

    public float spreadSpeed;
    float sizeX;
    float sizeY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        spreadSpeed = 300;
        sizeX = rectTransform.localScale.x;
        sizeY = rectTransform.localScale.y;

        StartCoroutine(DestroyObject());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpreadWave());
    }

    IEnumerator SpreadWave()
    {
        //transform.localScale += Vector3.one * spreadSpeed * Time.deltaTime;
        //sizeX = rectTransform.sizeDelta.x;
        //sizeX = rectTransform.localScale.x;
        //sizeY = rectTransform.sizeDelta.y;
        //sizeY = rectTransform.localScale.y;
        sizeX += spreadSpeed * Time.deltaTime;
        sizeY += spreadSpeed * Time.deltaTime;
        rectTransform.localScale = new Vector2(sizeX, sizeY);
        yield return null;
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5);
        StopAllCoroutines();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //석화상태부여
        //ex) player.status = petrified;
        if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject); //or setactive(false)
        }
    }

}
