using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;

public class CallInspectorPropertyDrawer
{
    [PublicAPI]
    public readonly List<CallInspectorProperty> callInspectorProperties = new List<CallInspectorProperty>();
    
    public CallInspectorPropertyDrawer(object target)
    {
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        var propertyInfos = target.GetType().GetFields(flags);

        foreach (FieldInfo fieldInfo in propertyInfos)
        {
            var buttonAttribute = fieldInfo.GetCustomAttribute<CallInSpectorAttribute>();
            if (buttonAttribute == null)
                continue;

            callInspectorProperties.Add(new CallInspectorProperty(fieldInfo, buttonAttribute));
        }
    }
    
    public void DrawProperties(object target)
    {
        if (callInspectorProperties.Count > 0)
        {
            GUILayout.Space(10);
            var skin = new GUIStyle(GUI.skin.label)
            {
                normal =
                {
                    textColor = Color.yellow
                },
                fontStyle = FontStyle.BoldAndItalic
            }; 
            
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Properties Attribute", skin);
            foreach (var property in callInspectorProperties)
            {
                property.Draw(target);
            }
            GUILayout.EndVertical();
        }
    }
}