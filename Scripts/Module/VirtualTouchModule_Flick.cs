using UnityEngine;

namespace Isshi777
{
    /// <summary>
    /// 「フリック」モジュール
    /// </summary>
    [DisallowMultipleComponent]
    public class VirtualTouchModule_Flick : AVirtualTouchMojule_OneFinger
    {
        /// <summary>
        /// フリック方向
        /// </summary>
        public enum EFlickDirection
        {
            UP,     // 上方向
            Down,   // 下方向
            Right,  // 右方向
            Left,   // 左方向
        }

        /// <summary>
        /// フリック判定時間（この時間以内だったらフリック判定）
        /// </summary>
        [SerializeField]
        private float judgeTime;

        /// <summary>
        /// フリックの判定距離（この距離以上だったらフリック判定）
        /// </summary>
        [SerializeField]
        private float judgeDistance;

        /// <summary>
        /// タッチしている時間（タイマー）
        /// </summary>
        private float timer;

        /// <summary>
        /// 有効であるか（有効時のみタイマー加算）
        /// </summary>
        private bool isValid;

        /// <summary>
        /// イベント
        /// </summary>
        /// <param name="flickDirection">フリック方向</param>
        public delegate void OnFlickEvent(EFlickDirection flickDirection);
        public OnFlickEvent OnFlick { set; get; }

        public override VirtualTouchPadConstants.ModuleType ModuleType => VirtualTouchPadConstants.ModuleType.Flick;


        protected override void OnTouchDown()
        {
            this.isValid = true;
        }

        protected override void OnTouching()
        {
            if (this.isValid)
            {
                this.timer += Time.deltaTime;
            }
        }

        protected override void OnTouchUp()
        {
            if (this.isValid)
            {
                if (this.timer <= this.judgeTime)
                {
                    Vector2 direction = this.touchUpPosition - this.touchDownPosition;
                    if (direction.magnitude >= this.judgeDistance)
                    {
                        var flickDirection = this.CalcFlickDirection(direction);
                        this.OnFlick?.Invoke(flickDirection);
                    }
                }
            }
        }

        /// <summary>
        /// フリックの方向を返す
        /// </summary>
        /// <param name="direction">方向ベクトル</param>
        /// <returns>フリックの方向</returns>
        private EFlickDirection CalcFlickDirection(Vector2 direction)
        {
            Vector2 absDirection = new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
            if (absDirection.x <= absDirection.y)// Y方向へのフリック
            {
                // 滅多にないと思うがイコールの時もこちらを通り、Y方向を優先させる
                return (direction.y < 0) ? EFlickDirection.Down : EFlickDirection.UP;
            }
            else// X方向へのフリック
            {
                return (direction.x < 0) ? EFlickDirection.Left : EFlickDirection.Right;
            }
        }

        protected override void Refresh()
        {
            this.timer = 0f;
            this.isValid = false;
        }
    }
}
