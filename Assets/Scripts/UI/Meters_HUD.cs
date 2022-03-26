using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meters_HUD : MonoBehaviour
{
    [SerializeField] RectTransform monsterMeterBar;
    [SerializeField] RectTransform curseMeterBar;
    [SerializeField] GameEvents gameEvents;

    private void ModifyMonsterMeterBar(float value)
    {
        //Given that the bar goes from 0 to 1, transform the value to adjust to that margins
        float scaledValue = Mathf.Clamp(value, 0f, 1f);
        monsterMeterBar.localScale = new Vector3(monsterMeterBar.localScale.x , scaledValue, monsterMeterBar.localScale.z);
    }

    private void ModifyCurseMeterBar(float value)
    {
        //Given that the bar goes from 0 to 1, transform the value to adjust to that margins
        float scaledValue = Mathf.Clamp(value, 0f, 1f);
        curseMeterBar.localScale = new Vector3(scaledValue, monsterMeterBar.localScale.y, monsterMeterBar.localScale.z);
    }

    #region EVENT SUBSCRIPTION
    private void OnEnable()
    {
        gameEvents.OnRaiseMonsterMeter += ModifyMonsterMeterBar;
        gameEvents.OnRaiseCurseMeter += ModifyCurseMeterBar;
    }

    private void OnDestroy()
    {
        gameEvents.OnRaiseMonsterMeter -= ModifyMonsterMeterBar;
        gameEvents.OnRaiseCurseMeter -= ModifyCurseMeterBar;
    }
    #endregion
}
