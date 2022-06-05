using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Controls_UI : MonoBehaviour
{

    [SerializeField] Image iconImage;

    #region LISTENERS
    private UnityAction<object> iconButtonListener;
    #endregion

    private void Awake()
    {
        iconButtonListener = new UnityAction<object>(ManageEvent);
    }

    void Start()
    {
        Type showInteractionType = typeof(ShowInteraction);
        EventManager.StartListening(showInteractionType, iconButtonListener);
    }

    private void ManageEvent(object argument)
    {
        switch (argument)
        {
            case ShowInteraction vartype:
                Debug.Log("Changing icon from action button");
                ChangeIcon(vartype.sprite);
                break;
        }
    }

    private void ChangeIcon(Sprite newSprite)
    {
        if (newSprite != null)
        {
            iconImage.gameObject.SetActive(true);
            iconImage.sprite = newSprite;
        }
        else
        {
            iconImage.gameObject.SetActive(false);
            iconImage.sprite = null;
        }
    }

    private void OnDisable()
    {
        Type showInteractionType = typeof(ShowInteraction);
        EventManager.StopListening(showInteractionType, iconButtonListener);
    }
}
