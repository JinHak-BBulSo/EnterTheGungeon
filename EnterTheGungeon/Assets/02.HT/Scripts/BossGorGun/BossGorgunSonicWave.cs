using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGorgunSonicWave : MonoBehaviour
{
    RectTransform rectTransform;
    CircleCollider2D circleCollider;

    public float spreadSpeed;
    float sizeX;
    float sizeY;

    // @brief for not collide check innerCircle
    float innerRadius;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        circleCollider = GetComponent<CircleCollider2D>();

        spreadSpeed = 300;

        StartCoroutine(DestroyObject());
    }

    // Update is called once per frame
    void Update()
    {
        circleCollider.radius = sizeX / 2;
        innerRadius = circleCollider.radius * 0.8f;

        StartCoroutine(SpreadWave());
    }

    IEnumerator SpreadWave()
    {
        //transform.localScale += Vector3.one * spreadSpeed * Time.deltaTime;
        sizeX = rectTransform.sizeDelta.x;
        sizeY = rectTransform.sizeDelta.y;
        sizeX += spreadSpeed * Time.deltaTime;
        sizeY += spreadSpeed * Time.deltaTime;
        rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
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
        float Distance_ = Vector2.Distance(other.transform.position, transform.position);

        if (Distance_ > innerRadius)
        {
            //석화상태부여
            //ex) player.status = petrified;

            //
            if (other.tag == "PlayerBullet")
            {
                Destroy(other.gameObject); //or setactive(false)
            }
        }
    }

}
