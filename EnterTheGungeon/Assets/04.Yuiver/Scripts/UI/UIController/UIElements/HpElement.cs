using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpElement : MonoBehaviour
{
    [SerializeField]
    Sprite[] HpImg = new Sprite[4]; /// 0이 빈거, 1이 반만 찬거, 2가 꽉찬거, 3이 실드

    Image hpImage = default;
    void Start()
    {
        hpImage = this.GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HpImgChanger(int index)
    {
        hpImage.sprite = HpImg[index];
    }
}
