using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PianoKey : MonoBehaviour
{
    [SerializeField] PianoKeyColor pianoKeyColor;
    Image img;
    [SerializeField][Range(0,100)] private float glowIntensity;
    [SerializeField] Color color;

    void Awake()
    {
        img = GetComponent<Image>();
        img.material = new Material(img.material);
    }

    void Start()
    {
        img = GetComponent<Image>();
        img.materialForRendering.SetFloat("_Glow", 0f);
        img.materialForRendering.SetColor("_GlowColor", color);
    }

    public void TurnOnKey()
    {
        if(pianoKeyColor == PianoKeyColor.Black) img.color = Color.white;
        img.materialForRendering.SetFloat("_Glow", glowIntensity);
    }

    public void TurnOffKey()
    {
        if (pianoKeyColor == PianoKeyColor.Black) img.color = Color.black;
        img.materialForRendering.SetFloat("_Glow", 0);
    }
}
public enum PianoKeyColor
{
    White = 0,
    Black = 1
}
