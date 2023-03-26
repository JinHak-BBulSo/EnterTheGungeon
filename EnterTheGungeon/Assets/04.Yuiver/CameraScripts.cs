using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraScripts : MonoBehaviour
{
    public static Texture2D[] cursorImg = default;

    void Start()
    {
        cursorImg = Resources.LoadAll<Texture2D>("MOUSE");
        Cursor.SetCursor(cursorImg[0],new Vector2(6.5f,6.5f),CursorMode.Auto);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCursor(int i)
    {
        Cursor.SetCursor(cursorImg[i], new Vector2(6.5f, 6.5f), CursorMode.Auto);
    }
}
