using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] RectTransform monsterMeterBar;
    [SerializeField] GameEvents gameEvents;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ModifyMonsterMeterBar(int value)
    {
        monsterMeterBar.localScale = new Vector3(monsterMeterBar.localScale.x, monsterMeterBar.localScale.y + .2f, monsterMeterBar.localScale.z);
    }

    private void OnEnable()
    {
        gameEvents.OnRaiseMonsterMeter += ModifyMonsterMeterBar;
    }

    private void OnDestroy()
    {
        
    }
}
