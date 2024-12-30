using System;
using CodeInUnity.Scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace CodeInUnity.Editor.CustomEditors
{
  [CustomEditor(typeof(StickyNoteScript))]
  public class StickyNoteCustomEditor : UnityEditor.Editor
  {
    private bool isEditing = false;

    private SerializedProperty text;

    private bool wasCancelled = false;


    void OnEnable()
    {
      this.text = base.serializedObject.FindProperty("text");
    }

    public override void OnInspectorGUI()
    {
      base.serializedObject.Update();

      //EditorGUILayout.LabelField("Audio Link Metadata", EditorStyles.boldLabel);

      if (this.isEditing)
      {
        base.OnInspectorGUI();
      }
      else
      {
        GUILayout.Label(this.text.stringValue, EditorStyles.helpBox);
      }

      this.DrawButtons();

      if(this.wasCancelled)
      {
        this.wasCancelled = false;
        return;
      }

      if (base.serializedObject.hasModifiedProperties)
      {
        base.serializedObject.ApplyModifiedProperties();
      }
    }

    private void DrawButtons()
    {
      if (this.isEditing)
      {
        if (GUILayout.Button("Save"))
        {
          this.isEditing = false;
        }

        if (GUILayout.Button("Cancel"))
        {
          this.isEditing = false;
          this.wasCancelled = true;
          base.serializedObject.Update();
        }
      }
      else
      {
        if (GUILayout.Button("Edit"))
        {
          this.isEditing = true;
        }
      }
    }
  }
}
