using UnityEngine;

namespace Isshi777
{
    /// <summary>
    /// 1本指のタッチモジュールの基底クラス
    /// </summary>
    public abstract class AVirtualTouchMojule_OneFinger : AVirtualTouchModule
    {
        /// <summary>
        /// タッチ（押した瞬間）した時の座標
        /// </summary>
        protected Vector2 touchDownPosition;

        /// <summary>
        /// 現在の座標（タッチしている間は更新され続ける）
        /// </summary>
        protected Vector2 currentPosition;

        /// <summary>
        /// 1フレーム前の座標（タッチしている間は更新され続ける）
        /// </summary>
        protected Vector2 lastPosition;

        /// <summary>
        /// タッチが終わった時の座標
        /// </summary>
        protected Vector2 touchUpPosition;

        protected override void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0)) //タップ押下時
            {
                if (this.IsInRange(Input.mousePosition))
                {
                    this.Refresh();

                    this.touchDownPosition = Input.mousePosition;
                    this.OnTouchDown();
                    this.lastPosition = this.touchDownPosition;
                }
            }
            else if (Input.GetMouseButton(0)) //タップ中（長押し含む）
            {
                if (this.IsInRange(Input.mousePosition))
                {
                    this.currentPosition = Input.mousePosition;
                    this.OnTouching();
                    this.lastPosition = this.currentPosition;
                }
            }
            else if (Input.GetMouseButtonUp(0)) //タップ終了
            {
                if (this.IsInRange(Input.mousePosition))
                {
                    this.touchUpPosition = Input.mousePosition;
                    this.OnTouchUp();
                }

                this.Refresh();
            }
        }

        /// <summary>
        /// タッチ（押した瞬間）した時の処理
        /// </summary>
        protected virtual void OnTouchDown()
        {
        }

        /// <summary>
        /// タッチしている間の処理
        /// </summary>
        protected virtual void OnTouching()
        {
        }

        /// <summary>
        /// タッチが終わった時の処理
        /// </summary>
        protected virtual void OnTouchUp()
        {
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected virtual void Refresh()
        {
            this.touchDownPosition = Vector2.zero;
            this.touchUpPosition = Vector2.zero;
            this.currentPosition = Vector2.zero;
            this.lastPosition = Vector2.zero;
        }

        protected override void OnRun()
        {
            this.Refresh();
        }

        protected override void OnStop()
        {
            this.Refresh();
        }
    }
}
