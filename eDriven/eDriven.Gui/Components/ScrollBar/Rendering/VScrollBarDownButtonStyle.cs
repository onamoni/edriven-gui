using eDriven.Gui.Graphics;
using eDriven.Gui.GUIStyles.Helper;
using UnityEngine;

namespace eDriven.Gui.Components.Rendering
{
    ///<summary>
    ///</summary>
    public class VScrollBarDownButtonStyle
    {
        #region Quasi-Singleton

        private static GUIStyle _instance;
        ///<summary>
        ///</summary>
        public static GUIStyle Instance
        {
            get
            {
                if (_instance == null)
                {
                    //Debug.Log("Getting VScrollBarDownButtonStyle instance");

                    _instance = new GUIStyle();
                    Initialize();
                }
                return _instance;
            }
        }

        private VScrollBarDownButtonStyle()
        {
            // constructor is protected
        }

        #endregion

        private static ProgramaticStyle _style;

        private const int Width = 30;
        private const int Weight = 1;

        private static void Initialize()
        {
            _style = new ProgramaticStyle {Style = _instance};
            ScrollbarHelper.ApplyButtonStyle(_style, Width, Weight, TriangleDirection.Down);
            _style.FixedWidth = Width;
            _style.FixedHeight = Width;
            _style.Commit();
        }
    }
}