using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(transform.localPosition.y - 300f, 2f, true).SetEase(Ease.InBack).SetAutoKill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
