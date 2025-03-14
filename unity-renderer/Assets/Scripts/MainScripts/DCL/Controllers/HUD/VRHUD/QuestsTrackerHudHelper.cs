//using DCL.Huds.QuestsTracker;
using SignupHUD;
using UnityEngine;

public class QuestsTrackerHudHelper : VRHUDHelper
{
    [SerializeField]
    //private QuestsTrackerHUDView view;
    private Transform camTrans;
    protected override void Awake()
    {
#if !DCL_VR
        return;
#endif
        base.Awake();
        myTrans = transform;
#if DCL_VR
        //view.OnSetVisibility += OnVisiblityChange;
#endif
        if (myTrans is RectTransform rect)
        {
            rect.sizeDelta = new Vector2(1920, 1080);
        }
    }

    protected override void SetupHelper() {
#if !DCL_VR
        return;
#endif
    }

    private void OnVisiblityChange(bool visible)
    {
#if !DCL_VR
        return;
#endif
        Position();
    }

    public void Position()
    {
#if !DCL_VR
        return;
#endif
        myTrans.localScale = 0.0024f*Vector3.one;
        var rawForward = CommonScriptableObjects.cameraForward.Get();
        var forward = new Vector3(rawForward.x, 0, rawForward.z).normalized;
        myTrans.position = Camera.main.transform.position + 3 * forward;// + 1.0f * Vector3.up;
        myTrans.forward =  forward;

    }
}
