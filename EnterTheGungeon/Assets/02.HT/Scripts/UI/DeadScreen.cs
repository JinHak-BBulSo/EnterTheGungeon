using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadScreen : MonoBehaviour
{
    Transform background;

    // { Var for color
    float backgroundColorRValue;
    float backgroundColorGValue;
    float backgroundColorBValue;
    float chageColorSpeed;
    bool isColorChange;
    // { Var for color

    // { Var for side
    RectTransform upside;
    RectTransform downside;
    public float spreadSizeSpeed;

    float upsideHeight;
    float downsideHeight;
    bool isSideSizeCheck;
    // } Var for side



    public bool isTest;
    // Start is called before the first frame update
    void Start()
    {
        background = transform.GetChild(0);
        background.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        chageColorSpeed = 1;
        spreadSizeSpeed = 300;
        upside = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        downside = transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        downside.sizeDelta = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isColorChange)
        {
            backgroundColorRValue = background.GetComponent<Image>().color.r;
            backgroundColorGValue = background.GetComponent<Image>().color.g;
            backgroundColorBValue = background.GetComponent<Image>().color.b;

            backgroundColorRValue -= chageColorSpeed * Time.deltaTime;
            backgroundColorGValue -= chageColorSpeed * Time.deltaTime;
            backgroundColorBValue -= chageColorSpeed * Time.deltaTime;
            background.GetComponent<Image>().color = new Color(backgroundColorRValue, backgroundColorGValue, backgroundColorBValue, 0.5f);
            if (backgroundColorRValue <= 0)
            {
                isColorChange = true;
            }
        }

        if (!isSideSizeCheck)
        {
            upsideHeight = upside.sizeDelta.y;
            downsideHeight = downside.sizeDelta.y;

            upsideHeight += spreadSizeSpeed * Time.deltaTime;
            downsideHeight += spreadSizeSpeed * Time.deltaTime;

            upside.sizeDelta = new Vector2(0, upsideHeight);
            downside.sizeDelta = new Vector2(0, downsideHeight);


            //upside.localPosition = new Vector3(0, -upsideHeight / 2f, 0);
            //downside.localPosition = new Vector3(0, downsideHeight / 2f, 0);

            if (upsideHeight >= 214)
            {
                isSideSizeCheck = true;
            }
        }

        if (isTest)
        {
            background.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            upside.sizeDelta = new Vector2(0, 0);
            downside.sizeDelta = new Vector2(0, 0);
            isColorChange = false;
            isSideSizeCheck = false;
            isTest = false;
        }
    }
}
