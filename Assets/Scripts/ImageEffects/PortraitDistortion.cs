using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitDistortion : MonoBehaviour
{
    Image img;

    void Start()
    {
        img = GetComponent<Image>();
        img.materialForRendering.SetFloat("_DistortSpeed", 1f);
        img.materialForRendering.SetFloat("_DistortAmount", 0f);
    }

    public void ChangeDistortion(float qty)
    {
        img.materialForRendering.SetFloat("_DistortAmount", qty);
        Debug.Log(img.materialForRendering.GetFloat("_DistortAmount"));
    }
}
