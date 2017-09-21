using System;
using UnityEngine;

public class HomeBaseView : NguiView
{
    [SerializeField] private UIButton _registrationButton;
    [SerializeField] private UIButton _signInButton;

    [SerializeField] private UserCredentialsView _userCredentialsView;

    private Action<CredentialsInputType, string, string> _onSetUserCredentials;

    private void Awake()
    {
        SetStatePanels(true);
    }

    public void InitializeViewData(HomeBaseRenderData data)
    {
        _onSetUserCredentials = data.SetUserCredentialsAction;
    }

    protected override void WireWidgets()
    {
        base.WireWidgets();

        EventDelegate.Add(_signInButton.onClick, () => OnShowForm(CredentialsInputType.SignIn));
        EventDelegate.Add(_registrationButton.onClick, () => OnShowForm(CredentialsInputType.Register));
    }

    protected override void OnRelease()
    {
        base.OnRelease();

        EventDelegate.Remove(_signInButton.onClick, () => OnShowForm(CredentialsInputType.SignIn));
        EventDelegate.Remove(_registrationButton.onClick, () => OnShowForm(CredentialsInputType.Register));
    }

    private void OnShowForm(CredentialsInputType type)
    {
        SetStatePanels(false);
        _userCredentialsView.Show(type, _onSetUserCredentials);
    }

    public void SetStatePanels(bool state)
    {
        _userCredentialsView.gameObject.SetActive(!state);
        _signInButton.transform.parent.gameObject.SetActive(state);
        _registrationButton.transform.parent.gameObject.SetActive(state);
    }
}
