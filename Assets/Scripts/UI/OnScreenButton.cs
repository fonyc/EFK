using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

[AddComponentMenu("Input/On-Screen Button")]
public class OnScreenButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite pressedSprite;
    [SerializeField] Image imageTarget;

    public void OnPointerUp(PointerEventData data)
    {
        imageTarget.sprite = normalSprite;
        SendValueToControl(0.0f);
    }

    public void OnPointerDown(PointerEventData data)
    {
        imageTarget.sprite = pressedSprite;
        SendValueToControl(1.0f);
    }

    [InputControl(layout = "Button")]
    [SerializeField]
    private string m_ControlPath;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }
}