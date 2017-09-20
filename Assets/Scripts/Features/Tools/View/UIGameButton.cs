using UnityEngine;

public class UIGameButton : MonoBehaviour
{
    [SerializeField] private UILabel _titleLabel;
    [SerializeField] private UILabel _priceLabel;

    public string Type { get; set; }

    private UIButton _button;

    public UIButton Button
    {
        get { return _button ?? (_button = GetComponent<UIButton>()); }
    }
    private BoxCollider _collider;

    public void SetPrice(int price)
    {
        _priceLabel.text = price.ToString();
    }
    public void SetTitle(string title)
    {
        _titleLabel.text = title;
    }

    public void EnableButton(bool state)
    {
        if (_button == null)
        {
            _button = GetComponent<UIButton>();
        }

        if (_collider == null)
        {
            _collider = GetComponent<BoxCollider>();
        }

        _collider.enabled = state;
        _button.SetState(state ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled, state);
    }
}
