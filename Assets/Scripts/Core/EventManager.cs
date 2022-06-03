using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UnityObjectEvent : UnityEvent<object> { }

public class EventManager : MonoBehaviour
{

    private Dictionary<object, UnityObjectEvent> eventDictionary;

    private static EventManager _instance;

    public static EventManager Instance { get { return _instance; } }

    private void Awake()
    {
        SingletonInit();
    }

    private void InitDictionary()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<object, UnityObjectEvent>();
        }
    }

    private void SingletonInit()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            InitDictionary();
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public static void StartListening(object eventKey, UnityAction<object> listener)
    {
        if (Instance == null) return;

        UnityObjectEvent currentEvent = null;

        if (Instance.eventDictionary.TryGetValue(eventKey, out currentEvent))
        {
            currentEvent.AddListener(listener);
        }
        else
        {
            currentEvent = new UnityObjectEvent();
            currentEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventKey, currentEvent);
        }
    }

    public static void StopListening(object eventKey, UnityAction<object> listener)
    {
        if (Instance == null) return;

        UnityObjectEvent currentEvent = null;

        if (Instance.eventDictionary.TryGetValue(eventKey, out currentEvent))
        {
            currentEvent.RemoveListener(listener);
        }
        else
        {
            Debug.LogWarning("There is no listener of type " + listener.GetType() + " to remove");
        }
    }

    public static void TriggerEvent(object arguments)
    {
        if (Instance == null) return;

        UnityObjectEvent currentEvent = null;
        object eventKey = arguments.GetType();

        if (Instance.eventDictionary.TryGetValue(eventKey, out currentEvent))
        {
            currentEvent.Invoke(arguments);
        }
        else
        {
            Debug.LogWarning("There are no listeners for event " + arguments + " of type " + arguments.GetType());
        }
    }
}