
using DG.Tweening;
using UnityEngine;

public class HomeBaseView : NguiView
{
    [SerializeField] private UILabel _startText;

    public void InitializeViewData(HomeBaseRenderData data)
    {
        _startText.text = data.StartText;
        _startText.transform.DOShakePosition(2, 10).SetLoops(-1);
    }

}
