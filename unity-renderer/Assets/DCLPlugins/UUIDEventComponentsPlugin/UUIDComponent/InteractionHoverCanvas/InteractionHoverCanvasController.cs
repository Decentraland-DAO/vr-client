using UnityEngine;
using TMPro;
using DCL;
using DCL.Helpers;

public class InteractionHoverCanvasController : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform backgroundTransform;
    public TextMeshProUGUI text;
    public GameObject[] icons;
    public RectTransform anchor;
    private Vector2 defaultAnchorOffset;
#if DCL_VR
    private Transform myTrans;
#endif

    bool isHovered = false;
    GameObject hoverIcon;
    private Vector3 meshCenteredPos;
#if DCL_VR
    [SerializeField]
    private float followSpeed = .8f;
    [SerializeField]
    private Vector3 offset = new Vector3(0f, .8f, 0f);
#endif
    const string ACTION_BUTTON_POINTER = "POINTER";
    const string ACTION_BUTTON_PRIMARY = "PRIMARY";
    const string ACTION_BUTTON_SECONDARY = "SECONDARY";

    private DataStore_Cursor dataStore;

    void Awake()
    {
        #if DCL_VR
        myTrans = transform;
        #endif
        defaultAnchorOffset = anchor.anchoredPosition;
        dataStore = DataStore.i.Get<DataStore_Cursor>();
        backgroundTransform.gameObject.SetActive(false);

        dataStore.hoverFeedbackButton.OnChange += OnChangeFeedbackButton;
        dataStore.hoverFeedbackText.OnChange += OnChangeFeedbackText;
        dataStore.hoverFeedbackEnabled.OnChange += OnChangeFeedbackEnabled;
        dataStore.hoverFeedbackHoverState.OnChange += OnChangeFeedbackHoverState;

        UpdateCanvas();
    }

    private void OnDestroy()
    {
        if (dataStore == null)
            return;

        dataStore.hoverFeedbackButton.OnChange -= OnChangeFeedbackButton;
        dataStore.hoverFeedbackText.OnChange -= OnChangeFeedbackText;
        dataStore.hoverFeedbackEnabled.OnChange -= OnChangeFeedbackEnabled;
        dataStore.hoverFeedbackHoverState.OnChange -= OnChangeFeedbackHoverState;
    }

    private void OnChangeFeedbackHoverState(bool current, bool previous)
    {
        SetHoverState(current);
    }

    private void OnChangeFeedbackEnabled(bool current, bool previous)
    {
        enabled = current;
        UpdateCanvas();
    }

    private void OnChangeFeedbackText(string current, string previous)
    {
        text.text = current;
        UpdateCanvas();
    }

    private void OnChangeFeedbackButton(string current, string previous)
    {
        ConfigureIcon(current);
        UpdateCanvas();
    }

    public void Setup(string button, string feedbackText)
    {
        text.text = feedbackText;
        ConfigureIcon(button);
        UpdateCanvas();
    }

    void ConfigureIcon(string button)
    {
        hoverIcon?.SetActive(false);

        switch (button)
        {
            case ACTION_BUTTON_POINTER:
                hoverIcon = icons[0];
                break;
            case ACTION_BUTTON_PRIMARY:
                hoverIcon = icons[1];
                break;
            case ACTION_BUTTON_SECONDARY:
                hoverIcon = icons[2];
                break;
            default: // ANY
                hoverIcon = icons[3];
                break;
        }

        hoverIcon.SetActive(true);
        backgroundTransform.gameObject.SetActive(true);
    }

    public void SetHoverState(bool hoverState)
    {
        if (!enabled || hoverState == isHovered)
            return;

        isHovered = hoverState;
        #if DCL_VR
        myTrans.position = CrossPlatformManager.GetPoint() + offset;
#endif
        UpdateCanvas();
    }

    public GameObject GetCurrentHoverIcon()
    {
        return hoverIcon;
    }

    void UpdateCanvas()
    {
        bool newValue = enabled && isHovered;

        if (canvas.enabled != newValue)
            canvas.enabled = newValue;
    }

    private void Update()
    {
#if DCL_VR
        if (!isHovered)
            return;
        Vector3 offsetPos = CrossPlatformManager.GetPoint() + offset;
        myTrans.position = Vector3.Lerp(myTrans.position, offsetPos, followSpeed);
        myTrans.forward = CommonScriptableObjects.cameraForward.Get();
#else
        if (Utils.IsCursorLocked)
        {
            anchor.anchoredPosition = defaultAnchorOffset;
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition, canvas.worldCamera,
            out Vector2 movePos);

        anchor.position = canvas.transform.TransformPoint(movePos);
        anchor.anchoredPosition += defaultAnchorOffset;
#endif
    }
}
