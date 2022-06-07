using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ErrorCounter : MonoBehaviour
{
    [SerializeField] Transform errorHolder;
    [SerializeField] Sprite noErrorSprite;
    [SerializeField] Sprite errorSprite;
    int maxErrors;
    int currentErrors;

    #region LISTENERS
    private UnityAction<object> paintErrorListener;
    #endregion

    private void Awake()
    {
        paintErrorListener = new UnityAction<object>(ManageEvent);
    }

    private void Start()
    {
        Type paintErrorType = typeof(PaintError);
        EventManager.StartListening(paintErrorType, paintErrorListener);

        currentErrors = 0;
        maxErrors = errorHolder.childCount;    
    }

    private void ManageEvent(object argument)
    {
        switch (argument)
        {
            case PaintError vartype:
                PaintError();
                break;
        }
    }

    private void PaintError()
    {
        if (currentErrors >= maxErrors || currentErrors < 0) return;
        
        errorHolder.GetChild(currentErrors).GetComponent<Image>().sprite = errorSprite;
        currentErrors++;
    }

    private void OnDisable()
    {
        Type paintErrorType = typeof(PaintError);
        EventManager.StopListening(paintErrorType, paintErrorListener);
    }
}
