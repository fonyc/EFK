using EFK.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private BaseStats _baseStats;
    [SerializeField] const float monsterMeterRefreshRate = 1f;
    [SerializeField] float monsterMeter = 0f;
    [SerializeField] float curseMeter = 0f;
    Coroutine monsterRaise_coro;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            if(player.TryGetComponent(out BaseStats baseStats))
            {
                _baseStats = baseStats;
            }
        }
    }

    private void Start()
    {
        Debug.Log(monsterMeter);
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
        //Call event to update UI display
        //Check if bar is full to begin Encounter

        Debug.Log(monsterMeter);

        StopCoroutine(monsterRaise_coro);
        monsterRaise_coro = null;
    }
    #endregion
}
