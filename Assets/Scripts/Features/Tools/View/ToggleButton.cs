using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIButton))]
public class ToggleButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> Active;
    [SerializeField] private List<GameObject> Deactive;

    private bool _state;
    public bool State { get { return _state; } }

    private UIButton _button;
    public UIButton Button { get { return _button; } }

    public void Awake()
    {
        SetState(false);
        _button = GetComponent<UIButton>();
    }

    public void SetState(bool state)
    {
        _state = state;

        foreach (GameObject activeGO in Active)
        {
            activeGO.SetActive(_state);
        }
        foreach (GameObject deactiveGO in Deactive)
        {
            deactiveGO.SetActive(!_state);
        }
    }
}
