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

    private void InitDictionary()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<object, UnityObjectEvent>();
        }
    }

    private void Awake()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Debug.Log("Already exists");

            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Doesn't yet exist");

            InitDictionary();
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
    }

    public static void StartListening(object eventKey, UnityAction<object> listener)
    {
        UnityObjectEvent currentEvent = null;

        Debug.Log(_instance==null);

        if (Instance.eventDictionary.TryGetValue(eventKey, out currentEvent))
        {
            currentEvent.AddListener(listener);
        }
        else
        {
            currentEvent = new UnityObjectEvent();
            currentEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventKey, currentEvent);
            Debug.Log(eventKey + " Event Key");
            Debug.Log(Instance.eventDictionary.TryGetValue(eventKey, out currentEvent) + "");
        }
    }

    public static void StopListening(object eventKey, UnityAction<object> listener)
    {
        if (_instance == null) return;

        UnityObjectEvent currentEvent = null;

        if (Instance.eventDictionary.TryGetValue(eventKey, out currentEvent))
        {
            currentEvent.RemoveListener(listener);
        }
        else
        {
            Debug.LogError("There is no listener to remove");
        }
    }

    public static void TriggerEvent(object eventKey, object arguments)
    {
        if (_instance == null) return;

        UnityObjectEvent currentEvent = null;

        if (Instance.eventDictionary.TryGetValue(eventKey, out currentEvent))
        {
            currentEvent.Invoke(arguments);
        }
        else
        {
            Debug.LogError("There is no listener to invoke");
        }
    }
}