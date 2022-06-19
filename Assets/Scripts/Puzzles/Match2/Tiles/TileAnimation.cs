using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TileAnimation : MonoBehaviour
{
    Image uiImage;
    [SerializeField] private float waveStrength;
    [SerializeField] private float waveSpeed;
    [SerializeField] private float timeToFullWave;
    [SerializeField] private float timeToNormal;
    [SerializeField] private float waitTime;

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

    private IEnumerator ShuffleAnimation(float targetWaveStrength)
    {
        GetComponent<InteractableTile>().IsBusy = true;
        float elapsedTime = 0;
        float currentStrengtWave = uiImage.materialForRendering.GetFloat("_RoundWaveStrength");

        while (elapsedTime < timeToFullWave)
        {
            float value = Mathf.Lerp(currentStrengtWave, targetWaveStrength, (elapsedTime / timeToFullWave));
            uiImage.materialForRendering.SetFloat("_RoundWaveStrength", value);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return waitTime;

        while (elapsedTime < timeToNormal)
        {
            float value = Mathf.Lerp(currentStrengtWave, targetWaveStrength, (elapsedTime / timeToFullWave));
            uiImage.materialForRendering.SetFloat("_RoundWaveStrength", value);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        GetComponent<InteractableTile>().IsBusy = false;
    }

}





