using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Meters_HUD : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] RectTransform monsterMeterBar;
    [SerializeField] RectTransform curseMeterBar;

    #region LISTENERS
    private UnityAction<object> monsterMeterListener;
    #endregion

    #endregion

    private void Awake()
    {
        monsterMeterListener = new UnityAction<object>(ManageEvent);
    }

    private void Start()
    {
        Type type = typeof(AddMonsterMeter);
        EventManager.StartListening(type, monsterMeterListener);
    }

    private void ManageEvent(object argument)
    {
        switch (argument)
        {
            case AddMonsterMeter varType:
                AddMonsterMeter amount = null;
                amount = (AddMonsterMeter)argument;
                ModifyMonsterMeterBar(amount.value);
                break;

            case AddCurseMeter vartype:
                Debug.Log("CM");
                break;
        }   
    }

    private void ModifyMonsterMeterBar(float value)
    {
        //Given that the bar ranges from 0 to 1, transform the value to adjust to that margins
        float scaledValue = Mathf.Clamp(value, 0f, 1f);
        monsterMeterBar.localScale = new Vector3(monsterMeterBar.localScale.x , scaledValue, monsterMeterBar.localScale.z);
    }

    private void ModifyCurseMeterBar(float value)
    {
        //Given that the bar ranges from 0 to 1, transform the value to adjust to that margins
        float scaledValue = Mathf.Clamp(value, 0f, 1f);
        curseMeterBar.localScale = new Vector3(scaledValue, monsterMeterBar.localScale.y, monsterMeterBar.localScale.z);
    }

    private void OnDestroy()
    {
        Type type = typeof(AddMonsterMeter);
        EventManager.StopListening(type, monsterMeterListener);
    }
}
