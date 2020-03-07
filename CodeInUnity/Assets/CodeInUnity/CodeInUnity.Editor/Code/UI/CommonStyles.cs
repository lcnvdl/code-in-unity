using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

namespace CodeInUnity.Editor.Code.UI
{
    public static class CommonStyles
    {
        public static GUIStyle LabelTitle
        {
            get
            {
                return LabelBold;
            }
        }

        public static GUIStyle LabelTitle2
        {
            get
            {
                return LabelBoldItalic;
            }
        }

        public static GUIStyle LabelBoldItalic
        {
            get
            {
                var style = new GUIStyle(GUI.skin.label);
                style.fontStyle = FontStyle.BoldAndItalic;
                return style;
            }
        }

        public static GUIStyle LabelNormal
        {
            get
            {
                return GUI.skin.label;
            }
        }

        public static GUIStyle LabelBold
        {
            get
            {
                var style = new GUIStyle(GUI.skin.label);
                style.fontStyle = FontStyle.Bold;
                return style;
            }
        }

        public static GUIStyle ButtonNormal
        {
            get
            {
                return GUI.skin.button;
            }
        }

        public static GUIStyle ButtonBold
        {
            get
            {
                var style = new GUIStyle(GUI.skin.button);
                style.fontStyle = FontStyle.Bold;
                return style;
            }
        }

        public static GUIStyle BoxWithoutBackground
        {
            get
            {
                var style = new GUIStyle(GUI.skin.box);
                style.normal.background = null;
                return style;
            }
        }
    }
}