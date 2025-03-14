using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))] [DisallowMultipleComponent]
public class ShowHideAnimator : MonoBehaviour
{
    private const float BASE_DURATION = 0.2f;

    public event System.Action<ShowHideAnimator> OnWillFinishHide;
    public event Action OnStartHide;
    public event System.Action<ShowHideAnimator> OnWillFinishStart;
    public event Action OnStartShow;
    //added for ease of triggering VR panel placement.
    public event System.Action<bool> OnSetVisibility;


    public bool hideOnEnable = true;
    public float animSpeedFactor = 1.0f;
    public bool disableAfterFadeOut;

    [SerializeField] private CanvasGroup canvasGroup;

    private GraphicRaycaster raycaster;

    public bool isVisible => canvasGroup == null || canvasGroup.blocksRaycasts;


    private void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        raycaster = GetComponent<GraphicRaycaster>();
    }

    private void OnEnable()
    {
        if (hideOnEnable)
            Hide(instant: true);
    }

    public void Show(bool instant = false)
    {
        //if (this.gameObject.name.Contains("Loading")) return;
        OnSetVisibility?.Invoke(true);
        SetVisibility(visible: true, OnShowCompleted, instant);

        void OnShowCompleted()
        {
            OnWillFinishStart?.Invoke(this);
        }
    }

    public void Hide(bool instant = false)
    {
        OnSetVisibility?.Invoke(true);
        SetVisibility(visible: false, OnHideCompleted, instant);

        void OnHideCompleted()
        {
            OnWillFinishHide?.Invoke(this);

            if (disableAfterFadeOut && gameObject != null)
                gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Show and then hide after delay (Show->Delay->Hide)
    /// </summary>
    public void ShowDelayHide(float delay)
    {
        SetVisibility(visible: true, onComplete: HideAfterDelay);

        void HideAfterDelay()
        {
            SetVisibility(visible: false, null).SetDelay(delay);
            OnWillFinishStart?.Invoke(this);
        }

}

    private TweenerCore<float, float, FloatOptions> SetVisibility(bool visible, TweenCallback onComplete, bool instant = false)
    {
        if (canvasGroup == null)
        {
            Debug.LogError($"Show Hide Animator in GameObject: {gameObject.name} has no canvasGroup assigned", gameObject);
            return null;
        }

        if (raycaster != null)
            raycaster.enabled = visible;

        // When instant, we use duration 0 instead of just modifying the canvas group to mock the old animator behaviour which needs a frame.
        float duration = instant ? 0 : BASE_DURATION * animSpeedFactor;

        canvasGroup.blocksRaycasts = visible;
        canvasGroup.DOKill();
        return canvasGroup.DOFade(visible ? 1 : 0, duration)
                   .SetEase(Ease.InOutQuad)
                   .OnComplete(onComplete)
                   .SetLink(canvasGroup.gameObject, LinkBehaviour.KillOnDestroy)
                   .SetLink(canvasGroup.gameObject, LinkBehaviour.KillOnDisable);
    }
}
