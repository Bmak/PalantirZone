using System;
using UnityEngine;

/// <summary>
/// Feature controller which controls behavior for the home base
/// </summary>
public class HomeBaseController : FeatureController, IOccludable
{
	[Inject] private PlayerRecordDomainController _playerDC;

	[Inject] private PlayerService _playerService;

    [Inject] private FirebaseAuthService _firebaseAuthService;

	private HomeBaseView _mainView;
	private HomeBaseTransitionInfo _transitionInfo;

	private int _widthScreen;
	private int _heightScreen;

	public void Initialize(HomeBaseTransitionInfo transitionInfo)
	{
		Initialize();

		_transitionInfo = transitionInfo;
	    LoadResources();
	}

	private void LoadResources()
	{
        _viewProvider.Get<HomeBaseView>(view => {
            _mainView = view;

            HomeBaseRenderData data = new HomeBaseRenderData
            {
                SetUserCredentialsAction = OnSetUserCredentials,
                
            };
            _mainView.InitializeViewData(data);
            ResourcesLoaded();
        });

    }

    private void OnSetUserCredentials(CredentialsInputType type, string email, string password)
    {
        _mainView.SetStatePanels(true);
        Log.Debug(String.Format("SET DATA: {0} / {1} / {2}", email, password, type));

        switch (type)
        {
            case CredentialsInputType.Register:
                _firebaseAuthService.AddNewUser(email, password);
                break;
            case CredentialsInputType.SignIn:
                _firebaseAuthService.SignInUser(email, password);
                break;
        }
    }

    private void ToggleMusicSwitched(bool value)
	{
		_audioSystem.SetMusicMuted(!value);
	}

	private void ToggleSoundSwitched(bool value)
	{
		_audioSystem.SetSoundMuted(!value);
	}

	private void EffectsSoundToggle()
	{
		_audioSystem.SetSoundMuted(!_audioSystem.GetSoundMuted());
	}

	private void MusicSoundToggle()
	{
		_audioSystem.SetMusicMuted(!_audioSystem.GetSoundMuted());
	}

	private void ResourcesLoaded()
	{
        _mainView.SetViewActive(true);
        if (_transitionInfo != null) {
		}
		FeatureInitializeFinish();
    }

	override protected void OnBackButtonClicked()
	{
		ExitUtil.MinimizeAndroidApplication();
	}

	public override void Shutdown()
	{
		base.Shutdown();

		if (_mainView != null) {
			_mainView.DeactivateAndRelease();
			_mainView = null;
		}
	}

	void IOccludable.OnOccluded()
	{
		if (_mainView != null) {
			_mainView.SetViewActive(false);
		}
	}

	void IOccludable.OnRevealed()
	{
		// Need to null check views here because if we deep link to certain areas (unit details),
		// we use the home base game state as a proxy since unit details has no state.
		// And if this gets called in that situation, home base view will be null

		if (_mainView != null) {
			_mainView.SetViewActive(true);
		}
	}

	void BackKeyPressed()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.Exit(0);
#elif UNITY_ANDROID
		AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		activity.Call<bool>("moveTaskToBack", true);
#endif
	}
}
