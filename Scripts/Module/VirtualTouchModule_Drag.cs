using UnityEngine;

/// <summary>
/// 「ドラッグ」モジュール
/// </summary>
[DisallowMultipleComponent]
public class VirtualTouchModule_Drag : AVirtualTouchMojule_OneFinger
{
    /// <summary>
    /// イベント
    /// </summary>
    /// <param name="direction">方向ベクトル</param>
    public delegate void OnDragEvent(Vector2 direction);
    public OnDragEvent OnDrag { set; get; }

    public override VirtualTouchPadConstants.ModuleType ModuleType => VirtualTouchPadConstants.ModuleType.Drag;

    protected override void OnTouching()
    {
        var direction = this.currentPosition - this.touchDownPosition;
        if (direction.magnitude > 0f)
        {
            this.OnDrag?.Invoke(direction);
        }
    }
}
