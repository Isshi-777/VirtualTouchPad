using UnityEngine;
/// <summary>
/// 「タッチ（押した際）」モジュール
/// </summary>
[DisallowMultipleComponent]
public class VirtualTouchModule_Press : AVirtualTouchMojule_OneFinger
{
    /// <summary>
    /// イベント
    /// </summary>
    public delegate void OnPressEvent();
    public OnPressEvent OnPress { set; get; }

    public override VirtualTouchPadConstants.ModuleType ModuleType => VirtualTouchPadConstants.ModuleType.Press;

    protected override void OnTouchDown()
    {
        this.OnPress?.Invoke();
    }
}
