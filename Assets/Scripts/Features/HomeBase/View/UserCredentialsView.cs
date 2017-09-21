using System;
using UnityEngine;

public enum CredentialsInputType
{
    Register,
    SignIn
}

public class UserCredentialsView : MonoBehaviour
{
    [SerializeField] private UILabel _emailLabel;
    [SerializeField] private UILabel _passwordLabel;

    [SerializeField] private UILocalizedLabel _buttonTitleLabel;
    [SerializeField] private UIButton _acceptButton;

    private Action<CredentialsInputType, string, string> _acceptAction;
    private CredentialsInputType _type;

    public void Show(CredentialsInputType type, Action<CredentialsInputType, string, string> acceptAction)
    {
        _acceptAction = acceptAction;
        _type = type;
        switch (type)
        {
            case CredentialsInputType.Register:
                _buttonTitleLabel.SetLocalizationKey("auth_new");
                break;
            case CredentialsInputType.SignIn:
                _buttonTitleLabel.SetLocalizationKey("auth_sign_in");
                break;
        }

        _emailLabel.text = "...";
        _passwordLabel.text = "...";

        EventDelegate.Add(_acceptButton.onClick, OnAccept);
    }

    private void OnAccept()
    {
        //TODO check current text
        if (_acceptAction != null)
        {
            _acceptAction(_type, _emailLabel.text, _passwordLabel.text);
        }
    }
}
