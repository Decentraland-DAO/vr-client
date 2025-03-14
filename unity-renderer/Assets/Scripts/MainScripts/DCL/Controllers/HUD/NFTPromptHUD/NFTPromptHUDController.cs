using DCL;
using DCL.Helpers.NFT;
using RPC.Context;
using UnityEngine;
using Object = UnityEngine.Object;

public class NFTPromptHUDController : IHUD
{
    #if DCL_VR
    internal const string VIEW_PREFAB_PATH = "NFTPromptHUDVR";
    #else
    internal const string VIEW_PREFAB_PATH = "NFTPromptHUD";
    #endif
    internal const string COULD_NOT_FETCH_NFT_FROM_API = "Couldn't fetch NFT: '{0}/{1}'.";
    internal const string DOES_NOT_SUPPORT_POLYGON = "Warning: OpenSea API does not support fetching Polygon assets.";

    internal INFTPromptHUDView view { get; private set; }

    private readonly OwnersInfoController ownersInfoController;
    private readonly RestrictedActionsContext restrictedActionsContext;
    private readonly BaseVariable<NFTPromptModel> openNftPromptVariable;

    private Coroutine fetchNFTRoutine = null;
    private NFTInfoSingleAsset? lastNFTInfo = null;

    private bool isPointerInTooltipArea = false;
    private bool isPointerInOwnerArea = false;

    public NFTPromptHUDController(RestrictedActionsContext restrictedActionsContext, BaseVariable<NFTPromptModel> openNftPromptVariable)
    {
        view = Object.Instantiate(Resources.Load<GameObject>(VIEW_PREFAB_PATH))
            .GetComponent<NFTPromptHUDView>();
        #if !DCL_VR
        view.SetActive(false);
        #else
        view.SetActive(true);
        #endif
        view.OnOwnerLabelPointerEnter += ShowOwnersTooltip;
        view.OnOwnerLabelPointerExit += TryHideOwnersTooltip;
        view.OnOwnersTooltipFocusLost += OnOwnersTooltipFocusLost;
        view.OnOwnersTooltipFocus += OnOwnersTooltipFocus;
        view.OnViewAllPressed += ShowOwnersPopup;
        view.OnOwnersPopupClosed += HideOwnersPopup;

        ownersInfoController = new OwnersInfoController(view.GetOwnerElementPrefab());

        this.openNftPromptVariable = openNftPromptVariable;
        openNftPromptVariable.OnChange += OpenNftInfoDialog;

        this.restrictedActionsContext = restrictedActionsContext;
        restrictedActionsContext.OpenNftPrompt += OpenPromptRequest;
    }

    public void OpenNftInfoDialog(NFTPromptModel model, NFTPromptModel prevModel)
    {
        if (!view.IsActive())
        {
            HideOwnersTooltip(true);
            HideOwnersPopup(true);

            if (fetchNFTRoutine != null)
                CoroutineStarter.Stop(fetchNFTRoutine);

            if (lastNFTInfo != null && lastNFTInfo.Value.Equals(model.contactAddress, model.tokenId))
            {
                SetNFT(lastNFTInfo.Value, model.comment, false);
                return;
            }

            view.SetLoading();

            fetchNFTRoutine = CoroutineStarter.Start(NFTUtils.FetchNFTInfoSingleAsset(model.contactAddress, model.tokenId,
                (nftInfo) => SetNFT(nftInfo, model.comment, true),
                (error) => view.OnError(string.Format(COULD_NOT_FETCH_NFT_FROM_API + " " + error + ". " + DOES_NOT_SUPPORT_POLYGON, model.contactAddress, model.tokenId))
            ));
        }
    }

    public void SetVisibility(bool visible)
    {
        view.SetActive(visible);

        if (!visible)
        {
            HideOwnersTooltip(true);
            HideOwnersPopup(true);
        }

        AudioScriptableObjects.dialogOpen.Play(true);
    }

    public void Dispose()
    {
        view.OnOwnerLabelPointerEnter -= ShowOwnersTooltip;
        view.OnOwnerLabelPointerExit -= TryHideOwnersTooltip;
        view.OnOwnersTooltipFocusLost -= OnOwnersTooltipFocusLost;
        view.OnOwnersTooltipFocus -= OnOwnersTooltipFocus;
        view.OnViewAllPressed -= ShowOwnersPopup;
        view.OnOwnersPopupClosed -= HideOwnersPopup;

        CoroutineStarter.Stop(fetchNFTRoutine);

        view?.Dispose();

        openNftPromptVariable.OnChange -= OpenNftInfoDialog;
        restrictedActionsContext.OpenNftPrompt -= OpenPromptRequest;
    }

    private void OpenPromptRequest(string contractAddress, string tokenId)
    {
        openNftPromptVariable.Set(new NFTPromptModel(contractAddress, tokenId, string.Empty));
    }

    private void SetNFT(NFTInfoSingleAsset info, string comment, bool shouldRefreshOwners)
    {
        view.SetActive(true);
        lastNFTInfo = info;
        view.SetNFTInfo(info, comment);
        if (shouldRefreshOwners)
        {
            ownersInfoController.SetOwners(info.owners);
        }
    }

    private void ShowOwnersTooltip()
    {
        isPointerInOwnerArea = true;

        if (lastNFTInfo == null || lastNFTInfo.Value.owners.Length <= 1)
            return;

        var tooltip = view.GetOwnersTooltip();

        if (tooltip.IsActive())
        {
            tooltip.KeepOnScreen();
            return;
        }

        tooltip.SetElements(ownersInfoController.GetElements());
        tooltip.Show();
    }

    void TryHideOwnersTooltip()
    {
        isPointerInOwnerArea = false;

        if (!isPointerInTooltipArea)
        {
            HideOwnersTooltip();
        }
    }

    void OnOwnersTooltipFocusLost()
    {
        isPointerInTooltipArea = false;
        if (!isPointerInOwnerArea)
        {
            HideOwnersTooltip();
        }
    }

    void OnOwnersTooltipFocus()
    {
        isPointerInTooltipArea = true;
        var tooltip = view.GetOwnersTooltip();
        if (tooltip.IsActive())
        {
            tooltip.KeepOnScreen();
        }
    }

    private void HideOwnersTooltip() { HideOwnersTooltip(false); }

    private void HideOwnersTooltip(bool instant)
    {
        var tooltip = view.GetOwnersTooltip();
        if (!tooltip.IsActive())
            return;

        tooltip.Hide(instant);
    }

    private void ShowOwnersPopup()
    {
        HideOwnersTooltip(true);
        isPointerInTooltipArea = false;
        isPointerInOwnerArea = false;

        var popup = view.GetOwnersPopup();

        if (popup.IsActive())
            return;

        popup.SetElements(ownersInfoController.GetElements());
        popup.Show();
    }

    private void HideOwnersPopup() { HideOwnersPopup(false); }

    private void HideOwnersPopup(bool instant) { view.GetOwnersPopup().Hide(instant); }

    internal static string FormatOwnerAddress(string address, int maxCharacters)
    {
        const string ellipsis = "...";

        if (address.Length <= maxCharacters)
        {
            return address;
        }
        else
        {
            int segmentLength = Mathf.FloorToInt((maxCharacters - ellipsis.Length) * 0.5f);
            return $"{address.Substring(0, segmentLength)}{ellipsis}{address.Substring(address.Length - segmentLength, segmentLength)}";
        }
    }
}
