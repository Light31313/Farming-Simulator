using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class CallInspectorProperty
{
    [PublicAPI] public readonly string displayName;
    /// <summary> MethodInfo object the button is attached to. </summary>
    [PublicAPI] public readonly FieldInfo field;

    public CallInspectorProperty(FieldInfo info,  CallInSpectorAttribute att)
    {
        displayName = string.IsNullOrEmpty(att.Name)
            ? ObjectNames.NicifyVariableName(info.Name)
            : att.Name;

        field = info;
    }

    public void Draw(object target)
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel(displayName);
        GUILayout.TextField(field.GetValue(target).ToString());
        GUILayout.EndHorizontal();
    }
}