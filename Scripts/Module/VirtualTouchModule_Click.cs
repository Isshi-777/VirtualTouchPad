using UnityEngine;

/// <summary>
/// 「クリック」モジュール
/// </summary>
[DisallowMultipleComponent]
public class VirtualTouchModule_Click : AVirtualTouchMojule_OneFinger
{
    /// <summary>
    /// クリック判定範囲
    /// </summary>
    [SerializeField]
    private float judgeRadius;

    /// <summary>
    /// イベント
    /// </summary>
    public delegate void OnClickEvent();
    public OnClickEvent OnClick { set; get; }

    /// <summary>
    /// 有効であるか（この値がFalseの時はイベントを呼べない）
    /// </summary>
    private bool isValid;

    public override VirtualTouchPadConstants.ModuleType ModuleType => VirtualTouchPadConstants.ModuleType.Click;

    protected override void OnTouchDown()
    {
        this.isValid = true;
    }

    protected override void OnTouching()
    {
        if (this.isValid)
        {
            var distance = this.GetDistance(this.touchDownPosition, this.currentPosition);
            if (distance > this.judgeRadius)
            {
                // 指定範囲を超えた場合は今回のタッチではイベントを呼べないようにする
                this.isValid = false;
                return;
            }
        }
    }

    protected override void OnTouchUp()
    {
        if (this.isValid)
        {
            var distance = this.GetDistance(this.touchUpPosition, this.touchDownPosition);
            if (distance <= this.judgeRadius)
            {
                this.isValid = false;
                this.OnClick?.Invoke();
            }
        }
    }

    protected override void Refresh()
    {
        this.isValid = false;
    }
}
