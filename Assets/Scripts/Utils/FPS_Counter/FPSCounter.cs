using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public float fps;
    public Text txt;

    private void Start()
    {
        InvokeRepeating("GetFPS",1,1);
    }

    public void GetFPS()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        txt.text = "FPS: " + fps.ToString();
    }

}
