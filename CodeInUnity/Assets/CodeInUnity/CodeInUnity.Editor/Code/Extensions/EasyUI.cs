using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CodeInUnity.Editor.Extensions
{
    public class Menu
    {
        public List<MenuItem> Items { get; set; }

        public Menu()
        {
            Items = new List<MenuItem>();
        }

        public Menu Show()
        {
            var rect = EditorGUILayout.GetControlRect();
            GenericMenu genericMenu = new GenericMenu();

            foreach (var item in Items)
            {
                genericMenu.AddItem(new GUIContent(item.Name), false, new GenericMenu.MenuFunction(item.Action));
            }

            genericMenu.DropDown(rect);
            //EditorGUIUtility.ExitGUI();

            return this;
        }

        public Menu Item(string name, Action action)
        {
            Items.Add(new MenuItem() { Name = name, Action = action });
            return this;
        }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public Action Action { get; set; }
    }

    public static class EasyUI
    {
        public static void ExpandVertical()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandHeight(true));
            EditorGUILayout.EndHorizontal();
        }
        public static void ExpandVertical(this EditorWindow window)
        {
            ExpandVertical();
        }
        public static Disposable BeginScrollView(this EditorWindow window, ref Vector2 scroll, params GUILayoutOption[] options)
        {
            return BeginScrollView(ref scroll, options);
        }
        public static Disposable BeginHorizontal(this EditorWindow window, params GUILayoutOption[] options)
        {
            return BeginHorizontal(options);
        }
        public static Disposable LabelWidth(this EditorWindow window, float width)
        {
            return LabelWidth(width);
        }
        public static Disposable BeginVertical(this EditorWindow window, params GUILayoutOption[] options)
        {
            return BeginVertical(options);
        }
        public static Disposable BeginScrollView(ref Vector2 scroll, params GUILayoutOption[] options)
        {
            scroll = EditorGUILayout.BeginScrollView(scroll, options);
            return new Disposable(() => { EditorGUILayout.EndScrollView(); });
        }
        public static Disposable LabelWidth(float width)
        {
            float backup = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = width;
            return new Disposable(() => { EditorGUIUtility.labelWidth = backup; });
        }
        public static Disposable BeginHorizontal(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal(options);
            return new Disposable(() => { EditorGUILayout.EndHorizontal(); });
        }
        public static Disposable BeginVertical(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(options);
            return new Disposable(() => EditorGUILayout.EndVertical());
        }

        public static Disposable EditMatrix()
        {
            var matrix = GUI.matrix;
            return new Disposable(() => GUI.matrix = matrix);
        }

        public static Menu Menu()
        {
            return new Menu();
        }
    }

    public class Disposable: IDisposable
    {
        Action action;

        public Disposable(Action action)
        {
            this.action = action;
        }

        public void Dispose()
        {
            if (action != null)
            {
                action();
            }
        }
    }
}
