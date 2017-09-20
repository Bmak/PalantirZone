
using UnityEngine;

public class UIButtonChanger : UIButton
{
    private UIButton[] _buttons;
    private void Start()
    {
        _buttons = GetComponentsInChildren<UIButton>();
    }

    public override void OnPress(bool isPressed)
    {
        base.OnPress(isPressed);

        foreach (UIButton button in _buttons)
        {
            if (button == this) continue;
            button.OnPress(isPressed);
            /*
            if (tweenTarget != null)
            {
                if (isPressed)
                {
                    button.SetState(State.Pressed, false);
                }
                else if (UICamera.currentTouch.current == gameObject)
                {
                    if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
                    {
                        button.SetState(State.Hover, false);
                    }
                    else if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.hoveredObject == gameObject)
                    {
                        button.SetState(State.Hover, false);
                    }
                    else button.SetState(State.Normal, false);
                }
                else button.SetState(State.Normal, false);
            }
            */
        }
    }
    public override void SetIsLock(bool lockState)
    {
        base.SetIsLock(lockState);
        if (_buttons == null)
            return;

        foreach (UIButton button in _buttons)
        {
            if (button == this) continue;
            button.SetIsLock(lockState);
        }
    }

    public override void SetBlockState(State blockState)
    {
        base.SetBlockState(blockState);
        if (_buttons == null)
            return;

        foreach (UIButton button in _buttons)
        {
            if (button == this) continue;
            button.SetBlockState(blockState);
        }
    }

    public override void SetState(State state, bool immediate)
    {
        base.SetState(state, immediate);

        if (_buttons == null)
            return;

        foreach (UIButton button in _buttons)
        {
            if (button == this) continue;
            button.SetState(state, immediate);
        }
    }
}
