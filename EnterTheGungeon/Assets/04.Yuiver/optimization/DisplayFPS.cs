using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayFPS : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;

    float frames = 0f;
    float timeElap = 0f;
    float frametime = 0;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        timeElap += Time.unscaledDeltaTime;
        if (timeElap > 1f)
        {
            frametime = timeElap / (float)frames;
            timeElap -= 1f;
            UpdateText();
            frames = 0;
        }
    }

    private void UpdateText()
    {
        text.text = string.Format($"Fps : {frames}, FrameTime : {frametime * 1000.0f:F10} ms");
    }
}
