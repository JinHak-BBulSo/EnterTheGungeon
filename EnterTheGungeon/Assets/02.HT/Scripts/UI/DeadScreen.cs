using System;
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
    public bool isSideSizeCheck;
    // } Var for side

    // { Var for ClockHair
    Transform player;
    Transform clockHair;
    RectTransform clockHairRectTransform;
    Image clockHairImage;
    Image hourHandImage;
    Image minuteHandImage;
    Image secondHandImage;
    RectTransform hourHandRectTransform;
    RectTransform minuteHandRectTransform;
    RectTransform secondHandRectTransform;
    // } Var for ClockHair

    public bool isShotEnd;


    public bool isTest;
    GameObject gamePause;
    GameObject ammonomicon;
    // Start is called before the first frame update
    void Start()
    {
        gamePause = transform.parent.GetChild(2).gameObject;
        ammonomicon = gamePause.transform.GetChild(1).gameObject;

        background = transform.GetChild(0);
        background.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        chageColorSpeed = 1;
        spreadSizeSpeed = 300;
        upside = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        downside = transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        downside.sizeDelta = Vector2.zero;

        //{ animation
        clockHair = transform.GetChild(1);

        clockHairRectTransform = clockHair.GetComponent<RectTransform>();
        clockHairImage = clockHair.GetComponent<Image>();
        player = GameObject.FindWithTag("Player").transform;


        hourHandImage = clockHair.GetChild(1).GetComponent<Image>();
        hourHandRectTransform = clockHair.GetChild(1).GetComponent<RectTransform>();
        minuteHandImage = clockHair.GetChild(0).GetComponent<Image>();
        minuteHandRectTransform = clockHair.GetChild(0).GetComponent<RectTransform>();
        secondHandImage = clockHair.GetChild(2).GetComponent<Image>();
        secondHandRectTransform = clockHair.GetChild(2).GetComponent<RectTransform>();

        //} animation

    }

    // Update is called once per frame
    void Update()
    {
        ImageSizeSet();

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

        if (isShotEnd)
        {
            StartCoroutine(LoadAmmonomicon());
        }
    }


    void ImageSizeSet()
    {
        clockHairImage.SetNativeSize();
        hourHandImage.SetNativeSize();
        minuteHandImage.SetNativeSize();
        secondHandImage.SetNativeSize();

        clockHairRectTransform.sizeDelta = new Vector2(clockHairRectTransform.sizeDelta.x * 3, clockHairRectTransform.sizeDelta.y * 3);
        hourHandRectTransform.sizeDelta = new Vector2(hourHandRectTransform.sizeDelta.x * 3, hourHandRectTransform.sizeDelta.y * 3);
        minuteHandRectTransform.sizeDelta = new Vector2(minuteHandRectTransform.sizeDelta.x * 3, minuteHandRectTransform.sizeDelta.y * 3);
        secondHandRectTransform.sizeDelta = new Vector2(secondHandRectTransform.sizeDelta.x * 3, secondHandRectTransform.sizeDelta.y * 3);
    }

    IEnumerator LoadAmmonomicon()
    {
        isShotEnd = false;
        yield return new WaitForSeconds(3);
        gamePause.SetActive(true);
        yield return null;
        ammonomicon.SetActive(true);
    }
}
