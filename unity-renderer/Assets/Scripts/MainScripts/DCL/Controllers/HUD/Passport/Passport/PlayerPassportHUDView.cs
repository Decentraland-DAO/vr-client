using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading;

namespace DCL.Social.Passports
{
    public class PlayerPassportHUDView : BaseComponentView, IPlayerPassportHUDView
    {
        [SerializeField] internal Button hideCardButton;
        [SerializeField] internal Button hideCardButtonGuest;
        [SerializeField] internal Button backgroundButton;
        [SerializeField] internal GameObject container;
        [SerializeField] internal RectTransform passportMask;
        [SerializeField] internal Canvas passportCanvas;
        [SerializeField] internal CanvasGroup passportCanvasGroup;

        public int PassportCurrentSortingOrder => passportCanvas.sortingOrder;

        public event Action OnClose;
        public event Action<bool> OnSetVisibility;
        private CancellationTokenSource animationCancellationToken = new CancellationTokenSource();
        private MouseCatcher mouseCatcher;


        #if DCL_VR
        public static PlayerPassportHUDView CreateView() =>
            Instantiate(Resources.Load<GameObject>("PlayerPassportVR")).GetComponent<PlayerPassportHUDView>();
        #else
        public static PlayerPassportHUDView CreateView() =>
            Instantiate(Resources.Load<GameObject>("PlayerPassport")).GetComponent<PlayerPassportHUDView>();
        #endif
        public void Initialize(MouseCatcher mouseCatcher)
        {
            hideCardButton.onClick.RemoveAllListeners();
            hideCardButton.onClick.AddListener(ClosePassport);
            hideCardButtonGuest.onClick.RemoveAllListeners();
            hideCardButtonGuest.onClick.AddListener(ClosePassport);
            backgroundButton.onClick.RemoveAllListeners();
            backgroundButton.onClick.AddListener(ClosePassport);
            this.mouseCatcher = mouseCatcher;

            if (mouseCatcher != null)
                mouseCatcher.OnMouseDown += ClosePassport;
        }

        public void SetVisibility(bool visible)
        {
            OnSetVisibility?.Invoke(visible);
            gameObject.SetActive(visible);
            #if DCL_VR
            Position();
            #endif
        }

        public void Position()
        {
            transform.localScale = 0.0020f*Vector3.one;
            var rawForward = CommonScriptableObjects.cameraForward.Get();
            var forward = new Vector3(rawForward.x, 0, rawForward.z).normalized;
            transform.position = Camera.main.transform.position + 3 * forward;// + 1.0f * Vector3.up;
            transform.forward =  forward;

        }

        public void SetPassportPanelVisibility(bool visible)
        {
            if (visible && mouseCatcher != null)
            {
                mouseCatcher.UnlockCursor();
            }
            OnSetVisibility?.Invoke(visible);
            animationCancellationToken.Cancel();
            animationCancellationToken = new CancellationTokenSource();

            if (visible)
            {
                container.SetActive(true);
                ShowPassportAnimation(animationCancellationToken.Token).Forget();
            }
            else
            {
                HidePassportAnimation(animationCancellationToken.Token);
            }

        }

        private async UniTaskVoid ShowPassportAnimation(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            passportMask.anchoredPosition = new Vector2(0, -180);
            passportCanvasGroup.alpha = 0;
            passportCanvasGroup.DOFade(1, 0.3f)
                          .SetEase(Ease.Linear);
            try
            {
                Vector2 endPosition = new Vector2(0, 0);
                Vector2 currentPosition = passportMask.anchoredPosition;
                DOTween.To(() => currentPosition, x => currentPosition = x, endPosition, 0.3f).SetEase(Ease.OutBack);
                while (passportMask.anchoredPosition.y < 0)
                {
                    passportMask.anchoredPosition = currentPosition;
                    await UniTask.NextFrame(cancellationToken);
                }
            }
            catch (OperationCanceledException) { }
        }

        private async UniTaskVoid HidePassportAnimation(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            passportMask.anchoredPosition = new Vector2(0, 0);
            passportCanvasGroup.alpha = 1;
            passportCanvasGroup.DOFade(0, 0.3f)
                          .SetEase(Ease.Linear);
            try
            {
                Vector2 endPosition = new Vector2(0, -180);
                Vector2 currentPosition = passportMask.anchoredPosition;
                DOTween.To(() => currentPosition, x => currentPosition = x, endPosition, 0.3f).SetEase(Ease.InBack);
                while (passportMask.anchoredPosition.y > -180)
                {
                    passportMask.anchoredPosition = currentPosition;
                    await UniTask.NextFrame(cancellationToken);
                }
                container.SetActive(false);
            }
            catch (OperationCanceledException)
            {
                Debug.LogError("Cancelled show passport animation");
            }
        }

        public override void RefreshControl()
        {
        }

        public override void Dispose()
        {
            if (mouseCatcher != null)
                mouseCatcher.OnMouseDown -= ClosePassport;

            animationCancellationToken?.Cancel();
            animationCancellationToken?.Dispose();
            animationCancellationToken = null;
        }

        private void ClosePassport()
        {
            OnClose?.Invoke();
        }
    }
}
