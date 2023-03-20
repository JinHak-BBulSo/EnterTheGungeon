using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpElement : MonoBehaviour
{
    [SerializeField]
    Sprite[] HpImg = new Sprite[3];

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
