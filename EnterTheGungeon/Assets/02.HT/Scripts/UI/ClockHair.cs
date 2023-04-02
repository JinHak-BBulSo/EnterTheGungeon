using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockHair : MonoBehaviour
{

    // { Var for ClockHair
    RectTransform rectTransform;
    Image image;
    Image hourHandImage;
    Image minuteHandImage;
    Image secondHandImage;

    float distanceToPlayer;
    float baseDistanceToPlayer;
    float distanceValueForAnimator;

    Vector2 directionToPlayer;

    bool isBaseDistanceCheck;
    Transform player;
    Animator animator;

    bool introEnd;

    float currentHour;
    float currentMinute;
    float currentSecond;
    float palyerDieSecond;
    RectTransform hourHandRectTransform;
    RectTransform minuteHandRectTransform;
    RectTransform secondHandRectTransform;

    Vector3 targetPos1;
    Vector3 targetPos2;

    int wobblePoint = 0;
    // } Var for ClockHair

    //test : gamemanager 등에서 플레이 타임 저장되게 한 후 참조하는 식으로 변경 예정
    float playeTime = 90;
    //test : gamemanager 등에서 플레이 타임 저장되게 한 후 참조하는 식으로 변경 예정
    DeadScreen deadScreen;

    //
    GameObject gamePause;
    //
    void Start()
    {
        gamePause = GameObject.Find("GamePause");

        deadScreen = base.transform.parent.GetComponent<DeadScreen>();

        //{ animation
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();


        hourHandImage = transform.GetChild(1).GetComponent<Image>();
        hourHandRectTransform = transform.GetChild(1).GetComponent<RectTransform>();
        minuteHandImage = transform.GetChild(0).GetComponent<Image>();
        minuteHandRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        secondHandImage = transform.GetChild(2).GetComponent<Image>();
        secondHandRectTransform = transform.GetChild(2).GetComponent<RectTransform>();


        currentHour = DateTime.Now.Hour % 12;
        currentMinute = DateTime.Now.Minute;
        currentSecond = DateTime.Now.Second;
        palyerDieSecond = currentSecond;
        //} animation

    }

    // Update is called once per frame
    void Update()
    {
        if (deadScreen.isSideSizeCheck)
        {

            if (!isBaseDistanceCheck)
            {
                baseDistanceToPlayer = Vector2.Distance(transform.position, Vector3.zero);
                isBaseDistanceCheck = true;
                //[KJH] ADD
                SoundManager.Instance.Play("GameOver/gameover_lockon_01_001", Sound.SFX);
            }
            distanceToPlayer = Vector2.Distance(transform.position, Vector3.zero);
            //directionToPlayer = clockHair.transform.position - Vector3.zero;


            if (distanceToPlayer > 0)
            {
                if (!introEnd)
                {
                    transform.position = Vector2.MoveTowards(transform.position, Vector3.zero, 7 * Time.deltaTime);
                    targetPos1 = new Vector2(transform.position.x - 0.5f, transform.position.y + 0.5f);
                    targetPos2 = new Vector2(transform.position.x + 0.25f, transform.position.y + 0.25f);
                }
            }
            if (distanceToPlayer <= 0.01f)
            {
                //[KJH] ADD
                SoundManager.Instance.Play("GameOver/gameover_reticle_01", Sound.SFX);
                introEnd = true;
            }

            if (introEnd == true)
            {

                if (currentSecond > palyerDieSecond - playeTime)
                {
                    currentSecond -= 50 * Time.deltaTime;
                    secondHandRectTransform.transform.localPosition = secondHandRectTransform.up * secondHandRectTransform.sizeDelta.y / 2;
                }
                else
                {
                    //clockHairAnimator.SetBool("isShot", true);
                }

                if (wobblePoint == 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPos1, 1 * Time.deltaTime);
                    if (transform.position == targetPos1)
                    {
                        wobblePoint = 1;
                    }
                }
                if (wobblePoint == 2)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPos2, 1 * Time.deltaTime);
                    if (transform.position == targetPos2)
                    {
                        wobblePoint = 3;
                    }
                }
                if (wobblePoint == 1 || wobblePoint == 3)
                {
                    transform.position = Vector2.MoveTowards(transform.position, Vector3.zero, 1 * Time.deltaTime);
                    if (transform.position == Vector3.zero)
                    {
                        wobblePoint++;
                    }
                }

                if (wobblePoint == 4)
                {
                    if(animator.GetBool("isShot") == false)
                    {
                        //[KJH] ADD
                        SoundManager.Instance.Play("GameOver/gameover_shot_01", Sound.SFX);
                    }
                    animator.SetBool("isShot", true);
                }
            }
        }

        //clockHair
        HandsRotateAndSetPosition();

    }

    void HandsRotateAndSetPosition()
    {
        //시침

        hourHandRectTransform.rotation = Quaternion.Euler(0, 0, -currentHour * 30f - currentMinute * 1 / 12);
        hourHandRectTransform.transform.localPosition = hourHandRectTransform.up * hourHandRectTransform.sizeDelta.y / 2;
        //분침
        minuteHandRectTransform.rotation = Quaternion.Euler(0, 0, -currentMinute * 6f);
        minuteHandRectTransform.transform.localPosition = minuteHandRectTransform.up * minuteHandRectTransform.sizeDelta.y / 2;
        //초침
        secondHandRectTransform.rotation = Quaternion.Euler(0, 0, -currentSecond * 6f);
        secondHandRectTransform.transform.localPosition = secondHandRectTransform.up * secondHandRectTransform.sizeDelta.y / 2;
    }

    public void ClockHairOff()
    {
        deadScreen.isShotEnd = true;
        gameObject.SetActive(false);
    }
}
