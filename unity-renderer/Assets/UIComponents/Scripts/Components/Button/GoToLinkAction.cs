using DCL.Interface;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[ExcludeFromCodeCoverage]
[RequireComponent(typeof(ButtonComponentView))]
public class GoToLinkAction : MonoBehaviour
{
    public string urlToGo;

    internal ButtonComponentView button;

    private void Awake()
    {
        button = GetComponent<ButtonComponentView>();

        if (button != null)
            button.onClick.AddListener(GoToUrl);
    }

    internal void GoToUrl()
    {
        if (string.IsNullOrEmpty(urlToGo))
            return;
#if DCL_VR
        WebInterface.OpenURL(urlToGo,true);
        #else
        WebInterface.OpenURL(urlToGo);
#endif
    }

    private void OnDestroy()
    {
        if (button != null)
            button.onClick.RemoveListener(GoToUrl);
    }
}
