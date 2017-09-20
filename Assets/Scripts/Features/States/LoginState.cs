using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoginState : State
{
    [Inject] private PlayerService _playerService;

    [Inject] private CoroutineCreator _coroutineCreator;

    [Inject] private AuthenticationDomainController _authenticationDC;

    [Inject] private UISystem _uiSystem;

    [Inject] private PlayerRecordDomainController _playerRecordDC;

    [Inject] private MessageController _messageController;

    [Inject] private LocalizationManager _localizationManager;


    private float _progress;
    const float _numCallbacks = 2f;

    // Behavior Overrides

    public override bool IsPreLogin()
    {
        return true;
    }

    public override bool SC_Enter(object transitionInfo, SC_Callback onCompleteCallback = null)
    {
        SetQualitySettings();
        //Log.DisableLogCategory(LogCategory.ALL);

        _progress = 0f;
        _uiSystem.SetOnProceed(ProceedToBaseState);

        UpdateProgress(1.0f, "load app");

        return true;
    }

    private void SetQualitySettings()
    {
        Application.targetFrameRate = 30;
        QualitySettings.antiAliasing = 4;
        QualitySettings.shadowCascades = 0;
        QualitySettings.shadowDistance = 15;
        QualitySettings.vSyncCount = 0;

#if UNITY_ANDROID || UNITY_IPHONE
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif
    }

    private void InitSocialService()
    {
        //TODO connect to FB, GooglePlay or AppleCenter
    }

    private void ProceedToBaseState()
    {
        _uiSystem.DestroyInitialLoadingSplash();

        HomeBaseTransitionInfo transitionInfo = new HomeBaseTransitionInfo();
        _stateController.EnterState<HomeBaseState>(transitionInfo);
    }

    private void UpdateProgress(float progress, string loadName)
    {
        _progress = progress;
        _uiSystem.UpdateProgressLoading(progress, loadName);
    }
}
