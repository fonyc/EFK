using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float timeValue;
    private float extraTimeValue;
    private int extraTimes;
    private const string timerFormat = "{0:00}:{1:00}";
    float curseMeterDamage;

    private bool isStopped;

    public bool IsStopped { get => isStopped; set => isStopped = value; }

    void Update()
    {
        if (IsStopped) return;
        if (timeValue != 0)
        {
            timeValue = timeValue > 0 ? timeValue - Time.deltaTime : 0;
            DisplayTime(timeValue);
        }
        else if (extraTimes > 0)
        {
            timeValue = extraTimeValue;
            extraTimes--;

            //Trigger Add Curse Meter
            AddCurseMeter addCurseMeterTrigger = new AddCurseMeter(curseMeterDamage);
            EventManager.TriggerEvent(addCurseMeterTrigger);
        }
        else
        {
            SolvePuzzle triggerTimeIsUp = new SolvePuzzle();
            EventManager.TriggerEvent(triggerTimeIsUp);
        }
    }

    private void DisplayTime(float displayedTime)
    {
        if (displayedTime < 0) displayedTime = 0;
        float minutes = Mathf.FloorToInt(displayedTime / 60);
        float seconds = Mathf.FloorToInt(displayedTime % 60);

        timerText.text = string.Format(timerFormat, minutes, seconds);
    }

    public void InitTimerVariables(TimerVariables values)
    {
        extraTimes = values.extraTimes;
        extraTimeValue = values.extraTimeValue;
        timeValue = values.timeValue;
        curseMeterDamage = values.curseMeterDamage;
    }
}

public struct TimerVariables
{
    public int extraTimes;
    public float extraTimeValue;
    public float timeValue;
    public float curseMeterDamage;

    public TimerVariables(int extraTimes, float extraTimeValue, float timeValue, float curseMeterDamage)
    {
        this.extraTimes = extraTimes;
        this.extraTimeValue = extraTimeValue;
        this.timeValue = timeValue;
        this.curseMeterDamage = curseMeterDamage;
    }
}
