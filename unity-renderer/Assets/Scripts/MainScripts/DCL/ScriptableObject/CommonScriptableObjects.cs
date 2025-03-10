using DCL.CameraTool;
using UnityEngine;

public static class NotificationScriptableObjects
{
    private static FloatVariable newApprovedFriendsValue;
    public static FloatVariable newApprovedFriends => CommonScriptableObjects.GetOrLoad(ref newApprovedFriendsValue, "ScriptableObjects/NotificationBadge_NewApprovedFriends");

    private static FloatVariable pendingChatMessagesValue;
    public static FloatVariable pendingChatMessages => CommonScriptableObjects.GetOrLoad(ref pendingChatMessagesValue, "ScriptableObjects/NotificationBadge_PendingChatMessages");

    private static FloatVariable pendingFriendRequestsValue;
    public static FloatVariable pendingFriendRequests => CommonScriptableObjects.GetOrLoad(ref pendingFriendRequestsValue, "ScriptableObjects/NotificationBadge_PendingFriendRequests");

    public static void UnloadAll()
    {
        Resources.UnloadAsset(newApprovedFriendsValue);
        Resources.UnloadAsset(pendingChatMessagesValue);
        Resources.UnloadAsset(pendingFriendRequestsValue);
    }
}

public static class AudioScriptableObjects
{
    // Common UI

    private static AudioEvent cameraFadeInEvent;
    public static AudioEvent cameraFadeIn => CommonScriptableObjects.GetOrLoad(ref cameraFadeInEvent, "ScriptableObjects/AudioEvents/HUDCommon/CameraFadeIn");

    private static AudioEvent cameraFadeOutEvent;
    public static AudioEvent cameraFadeOut => CommonScriptableObjects.GetOrLoad(ref cameraFadeOutEvent, "ScriptableObjects/AudioEvents/HUDCommon/CameraFadeOut");

    private static AudioEvent buttonHoverEvent;
    public static AudioEvent buttonHover => CommonScriptableObjects.GetOrLoad(ref buttonHoverEvent, "ScriptableObjects/AudioEvents/HUDCommon/ButtonHover");

    private static AudioEvent buttonClickEvent;
    public static AudioEvent buttonClick => CommonScriptableObjects.GetOrLoad(ref buttonClickEvent, "ScriptableObjects/AudioEvents/HUDCommon/ButtonClick");

    private static AudioEvent equipEvent;
    public static AudioEvent equip => CommonScriptableObjects.GetOrLoad(ref equipEvent, "ScriptableObjects/AudioEvents/HUDCommon/Equip");

    private static AudioEvent unequipEvent;
    public static AudioEvent unequip => CommonScriptableObjects.GetOrLoad(ref unequipEvent, "ScriptableObjects/AudioEvents/HUDCommon/Unequip");

    private static AudioEvent hideEvent;
    public static AudioEvent hide => CommonScriptableObjects.GetOrLoad(ref hideEvent, "ScriptableObjects/AudioEvents/HUDCommon/Hide");

    private static AudioEvent showEvent;
    public static AudioEvent show => CommonScriptableObjects.GetOrLoad(ref showEvent, "ScriptableObjects/AudioEvents/HUDCommon/Show");

    private static AudioEvent_WithRandomPitch inputEvent;
    public static AudioEvent_WithRandomPitch input => CommonScriptableObjects.GetOrLoad(ref inputEvent, "ScriptableObjects/AudioEvents/HUDCommon/InputField");

    private static AudioEvent buttonReleaseEvent;
    public static AudioEvent buttonRelease => CommonScriptableObjects.GetOrLoad(ref buttonReleaseEvent, "ScriptableObjects/AudioEvents/HUDCommon/ButtonRelease");

    private static AudioEvent cancelEvent;
    public static AudioEvent cancel => CommonScriptableObjects.GetOrLoad(ref cancelEvent, "ScriptableObjects/AudioEvents/HUDCommon/Cancel");

    private static AudioEvent confirmEvent;
    public static AudioEvent confirm => CommonScriptableObjects.GetOrLoad(ref confirmEvent, "ScriptableObjects/AudioEvents/HUDCommon/Confirm");

    private static AudioEvent dialogOpenEvent;
    public static AudioEvent dialogOpen => CommonScriptableObjects.GetOrLoad(ref dialogOpenEvent, "ScriptableObjects/AudioEvents/HUDCommon/DialogOpen");

    private static AudioEvent dialogCloseEvent;
    public static AudioEvent dialogClose => CommonScriptableObjects.GetOrLoad(ref dialogCloseEvent, "ScriptableObjects/AudioEvents/HUDCommon/DialogClose");

    private static AudioEvent enableEvent;
    public static AudioEvent enable => CommonScriptableObjects.GetOrLoad(ref enableEvent, "ScriptableObjects/AudioEvents/HUDCommon/Enable");

    private static AudioEvent errorEvent;
    public static AudioEvent error => CommonScriptableObjects.GetOrLoad(ref errorEvent, "ScriptableObjects/AudioEvents/HUDCommon/Error");

    private static AudioEvent disableEvent;
    public static AudioEvent disable => CommonScriptableObjects.GetOrLoad(ref disableEvent, "ScriptableObjects/AudioEvents/HUDCommon/Disable");

    private static AudioEvent fadeInEvent;
    public static AudioEvent fadeIn => CommonScriptableObjects.GetOrLoad(ref fadeInEvent, "ScriptableObjects/AudioEvents/HUDCommon/FadeIn");

    private static AudioEvent fadeOutEvent;
    public static AudioEvent fadeOut => CommonScriptableObjects.GetOrLoad(ref fadeOutEvent, "ScriptableObjects/AudioEvents/HUDCommon/FadeOut");

    private static AudioEvent_WithPitchIncrement listItemAppearEvent;
    public static AudioEvent_WithPitchIncrement listItemAppear => CommonScriptableObjects.GetOrLoad(ref listItemAppearEvent, "ScriptableObjects/AudioEvents/HUDCommon/ListItemAppear");

    private static AudioEvent chatReceiveGlobalEvent;
    public static AudioEvent chatReceiveGlobal => CommonScriptableObjects.GetOrLoad(ref chatReceiveGlobalEvent, "ScriptableObjects/AudioEvents/HUDCommon/ChatReceiveGlobal");

    private static AudioEvent chatReceivePrivateEvent;
    public static AudioEvent chatReceivePrivate => CommonScriptableObjects.GetOrLoad(ref chatReceivePrivateEvent, "ScriptableObjects/AudioEvents/HUDCommon/ChatReceivePrivate");

    private static AudioEvent chatReceiveMentionEvent;
    public static AudioEvent ChatReceiveMentionEvent => CommonScriptableObjects.GetOrLoad(ref chatReceiveMentionEvent, "ScriptableObjects/AudioEvents/HUDCommon/ChatReceiveMention");

    private static AudioEvent chatSendEvent;
    public static AudioEvent chatSend => CommonScriptableObjects.GetOrLoad(ref chatSendEvent, "ScriptableObjects/AudioEvents/HUDCommon/ChatSend");

    private static AudioEvent joinChannelEvent;
    public static AudioEvent joinChannel => CommonScriptableObjects.GetOrLoad(ref joinChannelEvent, "ScriptableObjects/AudioEvents/HUDCommon/JoinChannel");

    private static AudioEvent leaveChannelEvent;
    public static AudioEvent leaveChannel => CommonScriptableObjects.GetOrLoad(ref leaveChannelEvent, "ScriptableObjects/AudioEvents/HUDCommon/LeaveChannel");

    private static AudioEvent notificationEvent;
    public static AudioEvent notification => CommonScriptableObjects.GetOrLoad(ref notificationEvent, "ScriptableObjects/AudioEvents/HUDCommon/Notification");

    private static AudioEvent sliderValueChangeEvent;
    public static AudioEvent sliderValueChange => CommonScriptableObjects.GetOrLoad(ref sliderValueChangeEvent, "ScriptableObjects/AudioEvents/HUDCommon/SliderValueChange");

    private static AudioEvent inputFieldFocusEvent;
    public static AudioEvent inputFieldFocus => CommonScriptableObjects.GetOrLoad(ref inputFieldFocusEvent, "ScriptableObjects/AudioEvents/HUDCommon/InputFieldFocus");

    private static AudioEvent inputFieldUnfocusEvent;
    public static AudioEvent inputFieldUnfocus => CommonScriptableObjects.GetOrLoad(ref inputFieldUnfocusEvent, "ScriptableObjects/AudioEvents/HUDCommon/InputFieldUnfocus");

    private static AudioEvent UIHideEvent;
    public static AudioEvent UIHide => CommonScriptableObjects.GetOrLoad(ref UIHideEvent, "ScriptableObjects/AudioEvents/HUDCommon/UIHide");

    private static AudioEvent UIShowEvent;
    public static AudioEvent UIShow => CommonScriptableObjects.GetOrLoad(ref UIShowEvent, "ScriptableObjects/AudioEvents/HUDCommon/UIUnhide");

    private static AudioEvent tooltipPopupEvent;
    public static AudioEvent tooltipPopup => CommonScriptableObjects.GetOrLoad(ref tooltipPopupEvent, "ScriptableObjects/AudioEvents/HUDCommon/TooltipPopup");

    private static AudioEvent friendRequestEvent;
    public static AudioEvent FriendRequestEvent => CommonScriptableObjects.GetOrLoad(ref friendRequestEvent, "ScriptableObjects/AudioEvents/HUDCommon/FriendRequest");

    private static AudioEvent takeScreenshotEvent;
    public static AudioEvent takeScreenshot => CommonScriptableObjects.GetOrLoad(ref takeScreenshotEvent, "ScriptableObjects/AudioEvents/HUDCommon/TakeScreenshot");

    public static void UnloadAll()
    {
        Resources.UnloadAsset(cameraFadeInEvent);
        Resources.UnloadAsset(cameraFadeOutEvent);
        Resources.UnloadAsset(buttonHoverEvent);
        Resources.UnloadAsset(buttonClickEvent);
        Resources.UnloadAsset(equipEvent);
        Resources.UnloadAsset(unequipEvent);
        Resources.UnloadAsset(buttonReleaseEvent);
        Resources.UnloadAsset(cancelEvent);
        Resources.UnloadAsset(confirmEvent);
        Resources.UnloadAsset(dialogOpenEvent);
        Resources.UnloadAsset(dialogCloseEvent);
        Resources.UnloadAsset(enableEvent);
        Resources.UnloadAsset(errorEvent);
        Resources.UnloadAsset(disableEvent);
        Resources.UnloadAsset(fadeInEvent);
        Resources.UnloadAsset(fadeOutEvent);
        Resources.UnloadAsset(listItemAppearEvent);
        Resources.UnloadAsset(chatReceiveGlobalEvent);
        Resources.UnloadAsset(chatReceivePrivateEvent);
        Resources.UnloadAsset(chatReceiveMentionEvent);
        Resources.UnloadAsset(chatSendEvent);
        Resources.UnloadAsset(joinChannelEvent);
        Resources.UnloadAsset(leaveChannelEvent);
        Resources.UnloadAsset(notificationEvent);
        Resources.UnloadAsset(sliderValueChangeEvent);
        Resources.UnloadAsset(inputFieldFocusEvent);
        Resources.UnloadAsset(inputFieldUnfocusEvent);
        Resources.UnloadAsset(UIHideEvent);
        Resources.UnloadAsset(UIShowEvent);
        Resources.UnloadAsset(tooltipPopupEvent);
        Resources.UnloadAsset(friendRequestEvent);
    }
}

public static class CommonScriptableObjects
{
    private static Vector3Variable playerUnityPositionValue;
    public static Vector3Variable playerUnityPosition => GetOrLoad(ref playerUnityPositionValue, "ScriptableObjects/PlayerUnityPosition");

    private static Vector3Variable playerUnityEulerAnglesValue;
    public static Vector3Variable playerUnityEulerAngles => GetOrLoad(ref playerUnityEulerAnglesValue, "ScriptableObjects/PlayerUnityEulerAngles");

    private static Vector3Variable worldOffsetValue;
    public static Vector3Variable worldOffset => GetOrLoad(ref worldOffsetValue, "ScriptableObjects/WorldOffset");

    private static Vector2IntVariable playerCoordsValue;
    public static Vector2IntVariable playerCoords => GetOrLoad(ref playerCoordsValue, "ScriptableObjects/PlayerCoords");

    private static BooleanVariable playerIsOnMovingPlatformValue;
    public static BooleanVariable playerIsOnMovingPlatform => GetOrLoad(ref playerIsOnMovingPlatformValue, "ScriptableObjects/playerIsOnMovingPlatform");

    private static QuaternionVariable movingPlatformRotationDeltaValue;
    public static QuaternionVariable movingPlatformRotationDelta => GetOrLoad(ref movingPlatformRotationDeltaValue, "ScriptableObjects/MovingPlatformRotationDelta");

    // private static StringVariable sceneIDValue;
    // public static StringVariable sceneID => GetOrLoad(ref sceneIDValue, "ScriptableObjects/SceneID");
    private static IntVariable sceneNumbervalue;
    public static IntVariable sceneNumber => GetOrLoad(ref sceneNumbervalue, "ScriptableObjects/SceneNumber");

    private static FloatVariable minimapZoomValue;
    public static FloatVariable minimapZoom => GetOrLoad(ref minimapZoomValue, "ScriptableObjects/MinimapZoom");

    private static Vector3NullableVariable characterForwardValue;
    public static Vector3NullableVariable characterForward => GetOrLoad(ref characterForwardValue, "ScriptableObjects/CharacterForward");

    private static Vector3Variable cameraForwardValue;
    public static Vector3Variable cameraForward => GetOrLoad(ref cameraForwardValue, "ScriptableObjects/CameraForward");

    private static Vector3Variable cameraPositionValue;
    public static Vector3Variable cameraPosition => GetOrLoad(ref cameraPositionValue, "ScriptableObjects/CameraPosition");

    private static Vector3Variable cameraRightValue;
    public static Vector3Variable cameraRight => GetOrLoad(ref cameraRightValue, "ScriptableObjects/CameraRight");

    private static BooleanVariable cameraIsBlendingValue;
    public static BooleanVariable cameraIsBlending => GetOrLoad(ref cameraIsBlendingValue, "ScriptableObjects/CameraIsBlending");

    private static BooleanVariable cameraBlockedValue;
    public static BooleanVariable cameraBlocked => GetOrLoad(ref cameraBlockedValue, "ScriptableObjects/CameraBlocked");

    private static BooleanVariable playerInfoCardVisibleStateValue;
    public static BooleanVariable playerInfoCardVisibleState => GetOrLoad(ref playerInfoCardVisibleStateValue, "ScriptableObjects/PlayerInfoCardVisibleState");

    private static BooleanVariable forcePerformanceMeterValue;
    public static BooleanVariable forcePerformanceMeter => GetOrLoad(ref forcePerformanceMeterValue, "ScriptableObjects/ForcePerformanceMeter");

    public static RendererState rendererState => GetOrLoad(ref rendererStateValue, "ScriptableObjects/RendererState");
    private static RendererState rendererStateValue;

    public static BooleanVariable focusState => GetOrLoad(ref focusStateValue, "ScriptableObjects/FocusState");
    private static BooleanVariable focusStateValue;

    private static ReadMessagesDictionary lastReadChatMessagesDictionary;
    public static ReadMessagesDictionary lastReadChatMessages => GetOrLoad(ref lastReadChatMessagesDictionary, "ScriptableObjects/LastReadChatMessages");

    private static LongVariable lastReadChatMessagesValue;
    public static LongVariable lastReadWorldChatMessages => GetOrLoad(ref lastReadChatMessagesValue, "ScriptableObjects/LastReadWorldChatMessages");

    private static BooleanVariable allUIHiddenValue;
    public static BooleanVariable allUIHidden => GetOrLoad(ref allUIHiddenValue, "ScriptableObjects/AllUIHidden");
    private static BooleanVariable useInternalBrowserValue;
    public static BooleanVariable useInternalBrowser => GetOrLoad(ref useInternalBrowserValue, "ScriptableObjects/UseInternalBrowser");

    private static LatestOpenChatsList latestOpenChatsValue;
    public static LatestOpenChatsList latestOpenChats => GetOrLoad(ref latestOpenChatsValue, "ScriptableObjects/LatestOpenChats");

    private static CameraMode cameraModeValue;
    public static CameraMode cameraMode => GetOrLoad(ref cameraModeValue, "ScriptableObjects/CameraMode");

    private static BooleanVariable cameraModeInputLockedValue;
    public static BooleanVariable cameraModeInputLocked => GetOrLoad(ref cameraModeInputLockedValue, "ScriptableObjects/CameraModeInputLocked");

    private static BooleanVariable isProfileHUDOpenValue;
    public static BooleanVariable isProfileHUDOpen => GetOrLoad(ref isProfileHUDOpenValue, "ScriptableObjects/IsProfileHUDOpen");

    private static BooleanVariable isFullscreenHUDOpenValue;
    public static BooleanVariable isFullscreenHUDOpen => GetOrLoad(ref isFullscreenHUDOpenValue, "ScriptableObjects/IsAvatarHUDOpen");

    private static BooleanVariable isLoadingHUDOpenValue;
    public static BooleanVariable isLoadingHUDOpen => GetOrLoad(ref isLoadingHUDOpenValue, "ScriptableObjects/IsLoadingHUDOpen");

    private static BooleanVariable isTaskbarHUDInitializedValue;
    public static BooleanVariable isTaskbarHUDInitialized => GetOrLoad(ref isTaskbarHUDInitializedValue, "ScriptableObjects/IsTaskbarHUDInitialized");

    private static BooleanVariable tutorialActiveValue;
    public static BooleanVariable tutorialActive => GetOrLoad(ref tutorialActiveValue, "ScriptableObjects/TutorialActive");

    private static BooleanVariable featureKeyTriggersBlockedValue;
    public static BooleanVariable featureKeyTriggersBlocked => GetOrLoad(ref featureKeyTriggersBlockedValue, "ScriptableObjects/FeatureKeyTriggersBlocked");

    private static BooleanVariable userMovementKeysBlockedValue;
    public static BooleanVariable userMovementKeysBlocked => GetOrLoad(ref userMovementKeysBlockedValue, "ScriptableObjects/UserMovementKeysBlocked");

    private static BooleanVariable emailPromptActiveValue;
    public static BooleanVariable emailPromptActive => GetOrLoad(ref emailPromptActiveValue, "ScriptableObjects/EmailPromptActive");

    private static BooleanVariable voiceChatDisabledValue;
    public static BooleanVariable voiceChatDisabled => GetOrLoad(ref voiceChatDisabledValue, "ScriptableObjects/VoiceChatDisabled");

    private static BooleanVariable isScreenshotCameraActiveValue;
    public static BooleanVariable isScreenshotCameraActive => GetOrLoad(ref isScreenshotCameraActiveValue, "ScriptableObjects/IsScreenshotCameraActive");

    public static T GetOrLoad<T>(ref T variable, string path) where T : Object
    {
        if (variable == null)
        {
            variable = Resources.Load<T>(path);
        }

        return variable;
    }

    public static void UnloadAll()
    {
        Resources.UnloadAsset(playerUnityPositionValue);
        Resources.UnloadAsset(playerUnityEulerAnglesValue);
        Resources.UnloadAsset(worldOffsetValue);
        Resources.UnloadAsset(playerCoordsValue);
        Resources.UnloadAsset(playerIsOnMovingPlatformValue);
        Resources.UnloadAsset(movingPlatformRotationDeltaValue);
        Resources.UnloadAsset(sceneNumbervalue);
        Resources.UnloadAsset(minimapZoomValue);
        Resources.UnloadAsset(characterForwardValue);
        Resources.UnloadAsset(cameraForwardValue);
        Resources.UnloadAsset(cameraPositionValue);
        Resources.UnloadAsset(cameraRightValue);
        Resources.UnloadAsset(cameraIsBlendingValue);
        Resources.UnloadAsset(cameraBlockedValue);
        Resources.UnloadAsset(playerInfoCardVisibleStateValue);
        Resources.UnloadAsset(forcePerformanceMeterValue);
        Resources.UnloadAsset(rendererStateValue);
        Resources.UnloadAsset(focusStateValue);
        Resources.UnloadAsset(lastReadChatMessagesDictionary);
        Resources.UnloadAsset(lastReadChatMessagesValue);
        Resources.UnloadAsset(allUIHiddenValue);
        Resources.UnloadAsset(latestOpenChatsValue);
        Resources.UnloadAsset(cameraModeValue);
        Resources.UnloadAsset(cameraModeInputLockedValue);
        Resources.UnloadAsset(isProfileHUDOpenValue);
        Resources.UnloadAsset(isFullscreenHUDOpenValue);
        Resources.UnloadAsset(isTaskbarHUDInitializedValue);
        Resources.UnloadAsset(tutorialActiveValue);
        Resources.UnloadAsset(featureKeyTriggersBlockedValue);
        Resources.UnloadAsset(userMovementKeysBlockedValue);
        Resources.UnloadAsset(emailPromptActiveValue);
        Resources.UnloadAsset(voiceChatDisabledValue);
    }
}
