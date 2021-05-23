using UnityEngine;

namespace Isshi777
{
    /// <summary>
    /// VirtualTouchPadのサンプル
    /// </summary>
    public class VirtualTouchPadExample : MonoBehaviour
    {
        [SerializeField]
        private VIrtualTouchPad touchPad;

        private void Start()
        {
            {// Press
                VirtualTouchModule_Press module;
                if (this.touchPad.TryGetModule<VirtualTouchModule_Press>(VirtualTouchPadConstants.ModuleType.Press, out module))
                {
                    module.OnPress += () => Debug.Log(" Press !! ");
                }
            }

            {// LongPress
                VirtualTouchModule_LongPress module;
                if (this.touchPad.TryGetModule<VirtualTouchModule_LongPress>(VirtualTouchPadConstants.ModuleType.LongPress, out module))
                {
                    module.OnLongPress += () => Debug.Log(" LongPress !! ");
                }
            }

            {// Click
                VirtualTouchModule_Click module;
                if (this.touchPad.TryGetModule<VirtualTouchModule_Click>(VirtualTouchPadConstants.ModuleType.Click, out module))
                {
                    module.OnClick += () => Debug.Log(" Click !! ");
                }
            }

            {// Drag
                VirtualTouchModule_Drag module;
                if (this.touchPad.TryGetModule<VirtualTouchModule_Drag>(VirtualTouchPadConstants.ModuleType.Drag, out module))
                {
                    module.OnDrag += (direction) => Debug.Log(" Drag !! " + direction);
                }
            }

            {// Flick
                VirtualTouchModule_Flick module;
                if (this.touchPad.TryGetModule<VirtualTouchModule_Flick>(VirtualTouchPadConstants.ModuleType.Flick, out module))
                {
                    module.OnFlick += (directionType) => Debug.Log(" Flick !! " + directionType);
                }
            }
        }
    }
}
