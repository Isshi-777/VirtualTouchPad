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
    /// イベントを呼び出したか
    /// </summary>
    private bool isCalledEvent;

    /// <summary>
    /// タイマー
    /// </summary>
    private float timer;

    public override VirtualTouchPadConstants.ModuleType ModuleType => VirtualTouchPadConstants.ModuleType.LongPress;

    protected override void OnTouching()
    {
        // まだイベントを呼んでいない場合
        if (!this.isCalledEvent)
        {
            var distance = this.GetDistance(this.touchDownPosition, this.currentPosition);
            if (distance > this.judgeRadius)
            {
                // 指定範囲を超えた場合は今回のタッチではイベントを呼べないようにする
                this.isCalledEvent = true;
                return;
            }

            if (this.timer >= this.judgeTime && distance <= this.judgeRadius)
            {
                this.isCalledEvent = true;
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
        this.isCalledEvent = false;
    }
}
