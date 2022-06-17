using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileAnimation : MonoBehaviour
{
    Image uiImage;
    [SerializeField] private float waveStrength;
    [SerializeField] private float waveSpeed;

    void Awake()
    {
        uiImage = GetComponent<Image>();
        uiImage.material = new Material(uiImage.material);
    }

    public void ChangeWaveProperties(float newStrength, float newSpeed)
    {
        uiImage.materialForRendering.SetFloat("_RoundWaveStrength", waveStrength);
        uiImage.materialForRendering.SetFloat("_RoundWaveSpeed", waveSpeed);
    }

    public void AddWaveStrength(float qty)
    {
        float oldStrength = uiImage.materialForRendering.GetFloat("_RoundWaveStrength");
        uiImage.materialForRendering.SetFloat("_RoundWaveStrength", qty + oldStrength);
    }

    public void ChangeWaveStrength(float newStrength)
    {
        uiImage.materialForRendering.SetFloat("_RoundWaveStrength", waveStrength);
    }

    public void ChangeWaveSpeed(float newSpeed)
    {
        uiImage.materialForRendering.SetFloat("_RoundWaveSpeed", newSpeed);
    }

}





