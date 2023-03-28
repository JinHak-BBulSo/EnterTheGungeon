using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;

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


    // { Var for ScreenShot
    int resolutionWidth;
    int resolutionHeight;
    string path;

    bool isCaptured;

    public Sprite screenShot;
    // } Var for ScreenShot

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

        resolutionWidth = Screen.width;
        resolutionHeight = Screen.height;
        path = Application.dataPath + "/Resources/02.HT/ScreenShot/";



    }

    // Update is called once per frame
    void Update()
    {
        if (isTest)
        {

            if (!isCaptured)
            {
                ScreenShotDeadScreen();
            }
            else
            {

                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);

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

                /* if (isTest)
                {
                    background.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                    upside.sizeDelta = new Vector2(0, 0);
                    downside.sizeDelta = new Vector2(0, 0);
                    isColorChange = false;
                    isSideSizeCheck = false;


                    isTest = false;
                } */

                if (isShotEnd)
                {
                    StartCoroutine(LoadAmmonomicon());
                }
            }
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

    public void ScreenShotDeadScreen()
    {
        DirectoryInfo dir_ = new DirectoryInfo(path);
        if (!dir_.Exists)
        {
            Directory.CreateDirectory(path);
        }
        string screenShotName_;
        screenShotName_ = path + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";


        ScreenCapture.CaptureScreenshot(screenShotName_);

        Texture2D texture2D_ = ScreenCapture.CaptureScreenshotAsTexture();

        screenShot = Sprite.Create(texture2D_, new Rect(0, 0, texture2D_.width, texture2D_.height), new Vector2(0.5f, 0.5f));

        isCaptured = true;
    }

    public void Testtesttest()
    {
        // 캡처할 영역 크기
        Vector2 captureSize = new Vector2(300f, 200f);

        // 캡처할 영역의 중심 계산
        RectTransform canvasRT = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        Vector2 captureCenter = canvasRT.sizeDelta / 2f + new Vector2(0f, 27f);

        // 카메라 ViewportRect 계산
        Vector2 captureSizeRatio = new Vector2(captureSize.x / canvasRT.sizeDelta.x, captureSize.y / canvasRT.sizeDelta.y);
        Vector2 captureMin = captureCenter - canvasRT.sizeDelta / 2f * captureSizeRatio;
        Vector2 captureMax = captureCenter + canvasRT.sizeDelta / 2f * captureSizeRatio;
        Rect captureViewportRect = new Rect(captureMin.x / canvasRT.sizeDelta.x, captureMin.y / canvasRT.sizeDelta.y, captureSizeRatio.x, captureSizeRatio.y);

        // 캡처할 RenderTexture 생성
        RenderTexture renderTexture = new RenderTexture((int)captureSize.x, (int)captureSize.y, 24);

        // 카메라 ViewportRect, targetTexture 설정
        Camera.main.rect = captureViewportRect;
        Camera.main.targetTexture = renderTexture;

        // 화면 렌더링 후 텍스처로 캡처
        Camera.main.Render();
        Texture2D texture = new Texture2D((int)captureSize.x, (int)captureSize.y, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0f, 0f, captureSize.x, captureSize.y), 0, 0);
        texture.Apply();

        // 카메라 ViewportRect, targetTexture 설정 초기화
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
        Camera.main.targetTexture = null;
        RenderTexture.active = null;
    }
}
