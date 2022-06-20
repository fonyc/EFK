using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TileAnimation : MonoBehaviour
{
    Image tileImage;

    void Awake()
    {
        tileImage = GetComponent<Image>();
        tileImage.material = new Material(tileImage.material);
    }

    public void ChangeWaveProperties(float newStrength, float newSpeed)
    {
        tileImage.materialForRendering.SetFloat("_RoundWaveStrength", newStrength);
        tileImage.materialForRendering.SetFloat("_RoundWaveSpeed", newSpeed);
    }

    public void AddWaveStrength(float qty)
    {
        float oldStrength = tileImage.materialForRendering.GetFloat("_RoundWaveStrength");
        tileImage.materialForRendering.SetFloat("_RoundWaveStrength", qty + oldStrength);
    }

    public void ChangeWaveStrength(float newStrength)
    {
        tileImage.materialForRendering.SetFloat("_RoundWaveStrength", newStrength);
    }

    public void ChangeWaveSpeed(float newSpeed)
    {
        tileImage.materialForRendering.SetFloat("_RoundWaveSpeed", newSpeed);
    }

    //private IEnumerator ShuffleAnimation(float targetWaveStrength)
    //{
    //    GetComponent<InteractableTile>().IsBusy = true;
    //    float elapsedTime = 0;
    //    float currentStrengtWave = tileImage.materialForRendering.GetFloat("_RoundWaveStrength");

    //    while (elapsedTime < timeToFullWave)
    //    {
    //        float value = Mathf.Lerp(currentStrengtWave, targetWaveStrength, (elapsedTime / timeToFullWave));
    //        tileImage.materialForRendering.SetFloat("_RoundWaveStrength", value);
    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }

    //    yield return waitTime;

    //    while (elapsedTime < timeToNormal)
    //    {
    //        float value = Mathf.Lerp(currentStrengtWave, targetWaveStrength, (elapsedTime / timeToFullWave));
    //        tileImage.materialForRendering.SetFloat("_RoundWaveStrength", value);
    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }

    //    GetComponent<InteractableTile>().IsBusy = false;
    //}

}





