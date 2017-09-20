using System;
using UnityEngine;
using System.Collections.Generic;

public partial class Config
{
    private string _id = SystemInfo.deviceUniqueIdentifier;

	private bool _clientGameDataCachingEnabled = true;
	private bool _clientBootMenuEnabled = true;

	private bool _clientQuitConfirmationEnabled = false;
	private bool _clientCleanExitIosEnabled = true;
	private bool _clientCleanExitAndroidEnabled = false;

	private bool _clientRebootViaEmptyScene = true;

	private bool _clientInAppNotificationsEnabled = false;
	private bool _clientLocalPushNotesEnabled = true;
	private float _clientStoreReceiptRetryInterval = 180.0f;

	private int _clientTargetFrameRate = 30;
	private int _clientTargetFrameRateDebug = 60;
	private float _clientDefaultTimeScale = 1.0f;

	private string _rateThisAppAppleUrl = "https://itunes.apple.com/app/id123456789?ls=1&mt=8";
	private string _rateThisAppGoogleUrl = "https://play.google.com/store/apps/details?id=com.octobox.beatit";

	private string _serverUrl = "ws://188.187.52.225:2024/";

	// service prefabs
	private string _viewPrefabBusyWaitView = "ui.transitionui_busywaitview";
	private string _viewPrefabFastTransitionView = "ui.transitionui_fasttransitionview";
	private string _viewPrefabLoadingView = "ui.transitionui_loadingview";
	private string _uiGlobalBackdropPrefab = "ui.backdropcamera";

	private string _viewPrefabOneButtonMessage = "ui.onebuttonmessage_view";

	// View Prefabs
	private string _viewHomeBasePrefab = "ui.homebase_view";

    // unload unused assets frequency controls
    private bool _unloadUnusedAssetsEachBattle = true;
	private int _unloadUnusedAssetsEachStateFrequency = 0;


	private Color HexStringToColor(string hexColor)
	{
		return NGUIMath.HexToColor(Convert.ToUInt32(hexColor, 16));
	}

	private string RgbaToRGB(string hexColor)
	{
		return hexColor.Substring(2, 8);
	}

	// ** Begin Getters **

    public string Id
    {
        get { return _id; }
    }

	public bool GetClientGameDataCachingEnabled()
	{
		return _clientGameDataCachingEnabled;
	}

	public bool GetClientBootMenuEnabled()
	{
		return _clientBootMenuEnabled;
	}

	public bool GetClientQuitConfirmationEnabled()
	{
		return _clientQuitConfirmationEnabled;
	}

	public bool GetClientCleanExitIosEnabled()
	{
		return _clientCleanExitIosEnabled;
	}

	public bool GetClientCleanExitAndroidEnabled()
	{
		return _clientCleanExitAndroidEnabled;
	}

	public bool GetClientRebootViaEmptyScene()
	{
		return _clientRebootViaEmptyScene;
	}

	public bool GetClientInAppNotificationsEnabled()
	{
		return _clientInAppNotificationsEnabled;
	}

	public bool GetClientLocalPushNotesEnabled()
	{
		return _clientLocalPushNotesEnabled;
	}

	public float GetClientStoreReceiptRetryInterval()
	{
		return _clientStoreReceiptRetryInterval;
	}

	public int GetClientTargetFrameRate()
	{
		return _clientTargetFrameRate;
	}

	public int GetClientTargetFrameRateDebug()
	{
		return _clientTargetFrameRateDebug;
	}

	public float GetClientDefaultTimeScale()
	{
		return _clientDefaultTimeScale;
	}

	public string GetRateThisAppAppleUrl()
	{
		return _rateThisAppAppleUrl;
	}

	public string GetRateThisAppGoogleUrl()
	{
		return _rateThisAppGoogleUrl;
	}

	public string GetUIGlobalBackdropPrefab()
	{
		return _uiGlobalBackdropPrefab;
	}

    private Dictionary<System.Type, string> _viewPrefabDict;
    
	public string GetViewPrefabName(System.Type objType)
	{
		// TODO: Fix the constant re-creation of this dictionary.
		// A switch statement would even be more performant than this.

	    if (_viewPrefabDict == null)
	    {
            _viewPrefabDict = new Dictionary<System.Type, string> {
                { typeof(NguiBusyWaitView), _viewPrefabBusyWaitView },
                { typeof(NguiFastTransitionView), _viewPrefabFastTransitionView },
                { typeof(NguiLoadingView), _viewPrefabLoadingView },
                { typeof(HomeBaseView), _viewHomeBasePrefab },
                { typeof(OneButtonMessageView), _viewPrefabOneButtonMessage },
		    };
        }

		string prefabName = string.Empty;
		_viewPrefabDict.TryGetValue(objType, out prefabName);
		return prefabName;
	}

	public bool GetUnloadUnusedAssetsEachBattle()
	{
		return _unloadUnusedAssetsEachBattle;
	}

	public int GetUnloadUnusedAssetsEachStateFrequency()
	{
		return _unloadUnusedAssetsEachStateFrequency;
	}

	public string GetServerUrl()
	{
		return _serverUrl;
	}
}
