using EFK.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetersManager : MonoBehaviour
{
    [Header("--- METER STATS --- ")]
    private BaseStats _baseStats;
    [SerializeField] const int monsterMeterRefreshRate = 1;
    [SerializeField] float monsterMeter = 0f;
    [SerializeField] float curseMeter = 0f;
    Coroutine monsterRaise_coro;

     private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            if(player.TryGetComponent(out BaseStats baseStats)) _baseStats = baseStats;
        }
    }

    private void Update()
    {
        RaiseMonsterMeter();
    }

    #region RAISE CURSE METER

    //codigo muy chachi bueno!

    #endregion

    #region RAISE MONSTER METER
    private void RaiseMonsterMeter()
    {
        if(monsterRaise_coro == null)
        {
            monsterRaise_coro = StartCoroutine(RaiseMonsterMeter_Coro());
        }
    }

    private IEnumerator RaiseMonsterMeter_Coro()
    {
        yield return new WaitForSeconds(monsterMeterRefreshRate);

        float raiseValue = _baseStats.characters.GetStat(_baseStats.CHARACTERTYPE, Stat.MonsterMeterRaiseRate);
        float maxValue = _baseStats.characters.GetStat(_baseStats.CHARACTERTYPE, Stat.MonsterMeterMaxValue);
        monsterMeter += raiseValue / maxValue;

        AddMonsterMeter arguments = new AddMonsterMeter(monsterMeter);
        Type type = typeof(AddMonsterMeter);
        EventManager.TriggerEvent(type, arguments);

        //Check if bar is full to begin Encounter

        StopCoroutine(monsterRaise_coro);
        monsterRaise_coro = null;
    }
    #endregion
}
