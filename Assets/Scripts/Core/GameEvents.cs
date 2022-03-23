using EFK.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    [Header("--- METER STATS --- ")]
    private BaseStats _baseStats;
    [SerializeField] const int monsterMeterRefreshRate = 1;
    [SerializeField] int monsterMeter = 0;
    //[SerializeField] float curseMeter = 0f;
    Coroutine monsterRaise_coro;

    [Space(5)]
    [Header("--- EVENTS --- ")]
    [SerializeField] public Action<int> OnRaiseMonsterMeter;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            if(player.TryGetComponent(out BaseStats baseStats)) _baseStats = baseStats;
        }
    }

    private void Start()
    {
        Debug.Log(_baseStats.characters.GetLevelList(_baseStats.CHARACTERTYPE, 5));
    }

    private void Update()
    {
        RaiseMonsterMeter();
    }

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
        monsterMeter += _baseStats.characters.GetStat(_baseStats.CHARACTERTYPE, Stat.MonsterMeterRaiseRate);
        OnRaiseMonsterMeter?.Invoke(monsterMeter);
        //Check if bar is full to begin Encounter

        Debug.Log(monsterMeter);

        StopCoroutine(monsterRaise_coro);
        monsterRaise_coro = null;
    }
    #endregion
}
