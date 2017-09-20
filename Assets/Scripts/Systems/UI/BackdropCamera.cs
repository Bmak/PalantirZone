using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

/// <summary>
/// Controls the backdrop sprite for the global backdrop camera prefab.
/// </summary>
public class BackdropCamera : MonoBehaviour
{
    [Inject]
    LocalizationManager _lm;
    [Inject]
    LocalizationConfig _lc;

    [SerializeField]
	private UIProgressBar _progressBarLoading;
    [SerializeField]
    private UILabel _labelLoadingPercentage;
    [SerializeField]
    private UIButton _buttonProceed;
    [SerializeField]
    private UILabel _labelProceed;
    [SerializeField]
    private UISprite _fadeSprite;
    [SerializeField]
    private UISprite _flashSprite;
    [SerializeField]
    private UILabel _labelVersion;

    public Action OnProceedClicked;

    private Tweener tweener;

    public void Init()
    {
        Injector.VerifyInject(ref _lm);
        Injector.VerifyInject(ref _lc);

        _buttonProceed.gameObject.SetActive(false);
        _labelProceed.gameObject.SetActive(false);

        _labelVersion.text = String.Format("Version: {0}", Application.version);
    }

    public void WireWidgets()
    {
        EventDelegate.Add(_buttonProceed.onClick, ProceedClicked);
    }

    public void OnRelease()
    {
        EventDelegate.Remove(_buttonProceed.onClick, ProceedClicked);
    }

    private void ProceedClicked()
    {
		if (OnProceedClicked != null)
		{
			OnProceedClicked();
		}
    }

    public void UpdateProgressValue(float value, string loadName)
    {
        if (tweener != null)
        {
            DOTween.Kill(tweener.id);
        }
        tweener = DOTween.To(GetCurrentValue, f => UpdateProgress(f, loadName), value, 1.0f);
    }

    public float GetCurrentValue()
    {
        return _progressBarLoading.value;
    }

    public void UpdateProgress(float value, string name)
    {
        _progressBarLoading.value = value;
		_labelLoadingPercentage.text = string.Format("{1} {0}", value.ToString("P0"), name);

        if (value >= 1.0f)
        {
            _labelLoadingPercentage.gameObject.SetActive(true);
            //_buttonProceed.gameObject.SetActive(true);
            //_labelProceed.gameObject.SetActive(true);
            ProceedClicked();
        }
        else
        {
            _labelLoadingPercentage.gameObject.SetActive(true);
            _buttonProceed.gameObject.SetActive(false);
            _labelProceed.gameObject.SetActive(false);
        }
    }
}
