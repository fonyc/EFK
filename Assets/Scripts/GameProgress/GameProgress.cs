using System;
using UnityEngine;
using UnityEngine.Events;

public class GameProgress : MonoBehaviour
{
    [SerializeField] GameProgress_Data gameProgress;

    #region LISTENERS
    private UnityAction<object> addCurseMeterListener;
    #endregion

    private void Awake()
    {
        addCurseMeterListener = new UnityAction<object>(ManageEvent);
    }

    private void Start()
    {
        //EVENT SUBSCRIPTION
        Type type = typeof(AddCurseMeter);
        EventManager.StartListening(type, addCurseMeterListener);

        //Get gameProgressObject
        gameProgress = GameObject.FindGameObjectWithTag("GameProgressData")
            .GetComponent<GameProgress_Data>();
    }

    private void ManageEvent(object argument)
    {
        switch (argument)
        {
            case AddCurseMeter vartype:
                SetCurseMeter(vartype.value);

                //Invoke HUD event to update it
                RepaintCurseMeter repaintEvent = new RepaintCurseMeter(gameProgress.CurseMeter);
                EventManager.TriggerEvent(repaintEvent);
                break;
        }
    }

    #region GAME PROGRESS SETTERS
    private void SetCurseMeter(float value)
    {
        gameProgress.CurseMeter += value;
    }

    private void AddCollectible(int index)
    {
        gameProgress.CollectibleList[index] = true;
    }

    private void SetChoice(bool choice)
    {
        gameProgress.Choice = choice;
    }
    #endregion
}
