using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public Transform target = default;
    [SerializeField] public int rotateSpeed = default;

    private void Update()
    {
        LookAt();
    }

    private void LookAt()
    {
        if (target != null)
        {
            Vector2 direction = new Vector2(transform.position.x - target.position.x, transform.position.y - target.position.y);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 180f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
            transform.rotation = rotation;

            if (-90 < angle && angle < 90)
            {
            }
            else
            {
            }
        }
    }
}
