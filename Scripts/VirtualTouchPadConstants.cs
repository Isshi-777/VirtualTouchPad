namespace Isshi777
{
    /// <summary>
    /// VertualTouchPadの定数定義クラス
    /// </summary>
    public static class VirtualTouchPadConstants
    {
        /// <summary>
        /// モジュールタイプ
        /// </summary>
        public enum ModuleType
        {
            /// <summary>
            /// 押した際
            /// </summary>
            Press,

            /// <summary>
            /// 長押し
            /// </summary>
            LongPress,

            /// <summary>
            /// クリック
            /// </summary>
            Click,

            /// <summary>
            /// ドラッグ
            /// </summary>
            Drag,

            /// <summary>
            /// フリック
            /// </summary>
            Flick,
        }
    }
}
