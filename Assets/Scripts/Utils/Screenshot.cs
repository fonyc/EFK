#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class Screenshot
{
    [MenuItem("Tools/Screenshot/Take")]
    public static void TakeScreenShot()
    {
        ScreenCapture.CaptureScreenshot("Screenshot.png", 1);
    }
}
#endif