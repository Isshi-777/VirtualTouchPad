using UnityEngine;

/// <summary>
/// 仮想タッチパッド
///
/// 基本的に同じGameObjectにModuleをアタッチする
/// ModuleTypeの重複は不可
///
/// </summary>
[DisallowMultipleComponent]
public class VIrtualTouchPad : MonoBehaviour
{
    /// <summary>
    /// 参照するCanvas(管理下のModuleに対してのCanvas指定に使用)
    /// </summary>
    [SerializeField]
    private Canvas targetCanvas;

    /// <summary>
    /// 参照するRectTransform(管理下のModuleに対しての範囲指定に使用)
    /// </summary>
    [SerializeField]
    private RectTransform targetRtf;

    /// <summary>
    /// 管理下のModuleが参照するRectTransformを上書きするか
    /// </summary>
    [SerializeField]
    private bool overwriteCanvas;

    /// <summary>
    /// 管理下のModuleの参照するCanvasを上書きするか
    /// </summary>
    [SerializeField]
    private bool overwriteRtf;

    /// <summary>
    /// Moduleリスト
    /// </summary>
    private AVirtualTouchModule[] moduleList;

    /// <summary>
    /// 実行中かどうか
    /// </summary>
    public bool IsRunning { get; private set; }

    private void Awake()
    {
        // Module取得
        this.moduleList = gameObject.GetComponents<AVirtualTouchModule>();
        if (this.moduleList != null)
        {
            // 上書き設定があればRectTransform設定＆Canvas設定
            if (this.overwriteCanvas || this.overwriteRtf)
            {
                foreach (var module in this.moduleList)
                {
                    if (this.overwriteCanvas)
                    {
                        module.SetTargetCanvas(this.targetCanvas);
                    }
                    if (this.overwriteRtf)
                    {
                        module.SetTargetRtf(this.targetRtf);
                    }
                }
            }

            // 実行
            this.Run();
        }
    }

    /// <summary>
    /// 実行
    /// </summary>
    public void Run()
    {
        this.IsRunning = true;
        if (this.moduleList != null)
        {
            foreach (var module in this.moduleList)
            {
                module.Run();
            }
        }
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        this.IsRunning = false;
        if (this.moduleList != null)
        {
            foreach (var module in this.moduleList)
            {
                module.Stop();
            }
        }
    }

    /// <summary>
    /// Moduleを返す
    /// </summary>
    /// <typeparam name="T">AVirtualTouchModuleを継承したクラス</typeparam>
    /// <param name="moduleType">Moduleタイプ</param>
    /// <returns>Module</returns>
    public T GetModule<T>(VirtualTouchPadConstants.ModuleType moduleType) where T : AVirtualTouchModule
    {
        return this.GetModule(moduleType) as T;
    }

    /// <summary>
    ///Moduleを返す
    /// </summary>
    /// <param name="moduleType">Moduleタイプ</param>
    /// <returns>Module</returns>
    public AVirtualTouchModule GetModule(VirtualTouchPadConstants.ModuleType moduleType)
    {
        AVirtualTouchModule module = null;
        if (this.moduleList != null)
        {
            System.Array.Find(this.moduleList, x => x.ModuleType == moduleType);
        }

        if (module == null)
        {
            Debug.LogError(" Moduleが見つかりません！ " + moduleType);
        }

        return module;
    }
}
