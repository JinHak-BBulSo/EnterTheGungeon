using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Bullet : MonoBehaviour
{
    [SerializeField] public bool isRotate = default;

    private void Update()
    {
        if (isRotate)
        {
            transform.Rotate(Vector3.forward * 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
