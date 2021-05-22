using UnityEngine;

/// <summary>
/// 「長押し」モジュール
/// </summary>
[DisallowMultipleComponent]
public class VirtualTouchModule_LongPress : AVirtualTouchMojule_OneFinger
{
    /// <summary>
    /// 長押し判定範囲
    /// </summary>
    [SerializeField]
    private float judgeRadius;

    /// <summary>
    /// 長押し判定時間
    /// </summary>
    [SerializeField]
    private float judgeTime;

    /// <summary>
    /// イベント
    /// </summary>
    public delegate void OnLongPressEvent();
    public OnLongPressEvent OnLongPress { set; get; }

    /// <summary>
    /// 有効であるか
    /// </summary>
    private bool isValid;

    /// <summary>
    /// タイマー
    /// </summary>
    private float timer;

    public override VirtualTouchPadConstants.ModuleType ModuleType => VirtualTouchPadConstants.ModuleType.LongPress;

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

            if (this.timer >= this.judgeTime && distance <= this.judgeRadius)
            {
                this.isValid = false;
                this.OnLongPress?.Invoke();
                return;
            }

            this.timer += Time.deltaTime;
        }
    }

    protected override void Refresh()
    {
        base.Refresh();

        this.timer = 0f;
        this.isValid = false;
    }
}
