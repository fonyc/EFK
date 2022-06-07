﻿using UnityEngine;
using UnityEngine.UI;

namespace DragonMobileUI.Scripts
{
    public class SlidePanel : MonoBehaviour
    {
        public GameObject windows;
        public int activeWindow;
        public float slideSpeed = 0.5f;
        public GameObject togglePrefab;
        public RectTransform toggleContainer;

        private DrivenRectTransformTracker _tracker = new DrivenRectTransformTracker();
        private int _windowsCount;
        private Animation _windowsAnimator;
        private RectTransform _windowsRectTransform;

        public void GoToWindow(int index)
        {
            if (Application.isPlaying)
            {
                AnimateWindowChange(index);
            }
            else
            {
                SetupWindowsRectTransform();
            }
        }

        private void AnimateWindowChange(int index)
        {
            Keyframe[] minFrames = {new Keyframe(0f, -activeWindow), new Keyframe(slideSpeed, -index)};
            Keyframe[] maxFrames =
                {new Keyframe(0f, _windowsCount - activeWindow), new Keyframe(slideSpeed, _windowsCount - index)};
            var minCurve = new AnimationCurve(minFrames);
            var maxCurve = new AnimationCurve(maxFrames);
            var clip = new AnimationClip {legacy = true};
            clip.SetCurve("", typeof(RectTransform), "m_AnchorMin.x", minCurve);
            clip.SetCurve("", typeof(RectTransform), "m_AnchorMax.x", maxCurve);
            _windowsAnimator.AddClip(clip, "PanelAnimation");
            _windowsAnimator.Play("PanelAnimation");
            activeWindow = index;
        }

        private void Start()
        {
            UpdateState();
            SetupWindowsRectTransform();
            SetupToggleButtons(_windowsCount);
            SetupSwipeController();
        }

        private void SetupSwipeController()
        {
            var oneDirectionSwipeController = GetComponent<OneDirectionSwipeController>();
            if (oneDirectionSwipeController == null)
            {
                Debug.LogWarning("Swipe Controller is missing");
                return;
            }
            oneDirectionSwipeController.callback = OnSwipe;
        }

        private void OnSwipe(int direction)
        {
            if (activeWindow + direction < 0 || activeWindow + direction > _windowsCount - 1)
            {
                return;
            }
            toggleContainer.GetChild(activeWindow + direction).GetComponent<Toggle>().isOn = true;
        }

        private void OnValidate()
        {
            UpdateState();
            SetupToggleButtons(_windowsCount);
        }

        private void UpdateState()
        {
            if (windows == null || windows.GetComponent<RectTransform>() == null)
            {
                Debug.LogError("Windows panel instance is null or there is no required component");
                return;
            }
            _windowsAnimator = windows.GetComponent<Animation>();
            _windowsRectTransform = windows.GetComponent<RectTransform>();
            _windowsCount = _windowsRectTransform.childCount;
            if (activeWindow < 0 || activeWindow > _windowsCount - 1)
            {
                activeWindow = 0;
            }
            EnableWindowsParentRectTransform(false);
            GoToWindow(activeWindow);
        }

        private void SetupToggleButtons(int windowsCount)
        {
            DisableAllToggles();
            CreateNewTogglesIfRequired(windowsCount);
            ActivateTogglesBehaviour(windowsCount);
        }

        private void ActivateTogglesBehaviour(int windowsCount)
        {
            for (var i = 0; i < windowsCount; i++)
            {
                var onValueChanged = new Toggle.ToggleEvent();
                var windowIndex = i;
                onValueChanged.AddListener(arg0 =>
                {
                    if (arg0)
                    {
                        GoToWindow(windowIndex);
                    }
                });
                var toggle = toggleContainer.GetChild(i).GetComponent<Toggle>();
                toggle.onValueChanged = onValueChanged;
                toggle.isOn = i == activeWindow;
                toggleContainer.GetChild(i).gameObject.SetActive(true);
            }
        }

        private void CreateNewTogglesIfRequired(int windowsCount)
        {
            for (var i = 0; i < windowsCount - toggleContainer.childCount; i++)
            {
                var newButton = Instantiate(togglePrefab, toggleContainer);
                var toggleComponent = newButton.GetComponent<Toggle>();
                var toggleGroup = toggleContainer.gameObject.GetComponent<ToggleGroup>();
                if (toggleComponent == null || toggleGroup == null) continue;
                toggleComponent.group = toggleGroup;
            }
        }

        private void DisableAllToggles()
        {
            for (var i = 0; i < toggleContainer.childCount; i++)
            {
                toggleContainer.GetChild(i).gameObject.SetActive(false);
                toggleContainer.GetChild(i).GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
            }
        }

        private void SetupWindowsRectTransform()
        {
            _windowsRectTransform.pivot = Vector2.zero;
            _windowsRectTransform.offsetMin = Vector2.zero;
            _windowsRectTransform.offsetMax = Vector2.zero;
            _windowsRectTransform.localScale = Vector3.one;
            _windowsRectTransform.anchorMin = new Vector2(-activeWindow, 0);
            _windowsRectTransform.anchorMax = new Vector2(_windowsCount - activeWindow, 1);
        }

        private void EnableWindowsParentRectTransform(bool enable)
        {
            _tracker.Clear();
            if (!enable) {
                _tracker.Add(this, _windowsRectTransform, DrivenTransformProperties.All);
            }
            
        }
        private void OnDisable()
        {
            EnableWindowsParentRectTransform(true);
        }
    }

}
