using UnityEngine;

namespace Isshi777
{
    /// <summary>
    /// モジュールの基底クラス
    /// </summary>
    public abstract class AVirtualTouchModule : MonoBehaviour
    {
        /// <summary>
        /// 参照するCanvas
        /// </summary>
        [SerializeField]
        private Canvas targetCanvas;

        /// <summary>
        /// 参照するRectTransform(指定範囲を指す)
        /// </summary>
        [SerializeField]
        protected RectTransform targetRtf;

        /// <summary>
        /// 実行中かどうか
        /// </summary>
        public bool IsRunning { set; private get; }

        /// <summary>
        /// モジュールのタイプ
        /// </summary>
        public abstract VirtualTouchPadConstants.ModuleType ModuleType { get; }

        /// <summary>
        /// 範囲内であるか
        /// </summary>
        /// <param name="position">スクリーン座標</param>
        /// <returns>範囲内:True 範囲外:False</returns>
        protected virtual bool IsInRange(Vector2 position)
        {
            // CanvasまたはRectTransformを参照していない場合は指定範囲は画面全体を指す
            if (this.targetRtf == null || this.targetCanvas == null)
            {
                return true;
            }

            // タッチした座標が指定したRectTransform範囲内であるか（CanvasのRenderModeを確認）
            switch (this.targetCanvas.renderMode)
            {
                case RenderMode.ScreenSpaceOverlay:
                    return RectTransformUtility.RectangleContainsScreenPoint(this.targetRtf, position);
                case RenderMode.ScreenSpaceCamera:
                case RenderMode.WorldSpace:
                    return RectTransformUtility.RectangleContainsScreenPoint(this.targetRtf, position, this.targetCanvas.worldCamera);
            }

            return false;
        }

        /// <summary>
        /// 使用するCanvasを設定する（外部から設定したい場合に利用）
        /// </summary>
        /// <param name="canvas">Canvas</param>
        public void SetTargetCanvas(Canvas canvas)
        {
            this.targetCanvas = canvas;
        }

        /// <summary>
        /// 使用するRectTransformを設定する（外部から設定したい場合に利用）
        /// </summary>
        /// <param name="rtf">RectTransform</param>
        public void SetTargetRtf(RectTransform rtf)
        {
            this.targetRtf = rtf;
        }

        /// <summary>
        /// 座標間の距離を返す
        /// </summary>
        /// <param name="pos1">座標１</param>
        /// <param name="pos2">座標２</param>
        /// <returns>距離</returns>
        protected float GetDistance(Vector2 pos1, Vector2 pos2)
        {
            return (pos2 - pos1).magnitude;
        }

        /// <summary>
        /// 座標間の中点座標を返す
        /// </summary>
        /// <param name="pos1">座標１</param>
        /// <param name="pos2">座標２</param>
        /// <returns>座標間の中点</returns>
        protected Vector2 GetCenterPosition(Vector2 pos1, Vector2 pos2)
        {
            float x = (pos1.x + pos2.x) / 2f;
            float y = (pos1.y + pos2.y) / 2f;
            return new Vector2(x, y);
        }

        private void Update()
        {
            if (this.IsRunning)
            {
                this.OnUpdate();
            }
        }

        /// <summary>
        /// 実行
        /// </summary>
        public void Run()
        {
            this.IsRunning = true;
            this.OnRun();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this.IsRunning = false;
            this.OnStop();
        }

        /// <summary>
        /// 実行時の処理
        /// </summary>
        protected virtual void OnRun()
        {
        }

        /// <summary>
        /// 停止時の処理
        /// </summary>
        protected virtual void OnStop()
        {
        }

        /// <summary>
        /// 毎フレーム処理
        /// </summary>
        protected abstract void OnUpdate();
    }
}
