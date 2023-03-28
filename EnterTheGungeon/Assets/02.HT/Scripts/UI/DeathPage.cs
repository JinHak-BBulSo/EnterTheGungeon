using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPage : MonoBehaviour
{
    DeadScreen deadScreen;

    Transform deathPage2;
    Image deadScreenShot;

    // Start is called before the first frame update
    void Start()
    {
        deadScreen = transform.parent.parent.parent.parent.parent.GetChild(0).GetComponent<DeadScreen>();
        
        deathPage2 = transform.parent.parent.GetChild(1).GetChild(1);
        deadScreenShot = deathPage2.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        deadScreenShot.sprite = deadScreen.screenShot;
    }
}
