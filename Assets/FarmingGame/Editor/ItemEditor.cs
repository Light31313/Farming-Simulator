using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CustomEditor(typeof(ItemConfig)), CanEditMultipleObjects]
public class ItemEditor : Editor
{
    public SerializedProperty
        tagsProp,
        itemNameProp,
        itemDesProp,
        spriteItemProp,
        maxStackProp,
        typeProp,
        needIndicatorProp,
        spritePlantGrowsProp,
        harvestPriceProp,
        attackDamageProp,
        canHarvestMultipleTimesProp,
        spriteHarvestMultipleTimesProp;

    void OnEnable()
    {
        // Setup the SerializedProperties
        tagsProp = serializedObject.FindProperty("tags");
        itemNameProp = serializedObject.FindProperty("itemName");
        itemDesProp = serializedObject.FindProperty("itemDescription");
        spriteItemProp = serializedObject.FindProperty("spriteItem");
        maxStackProp = serializedObject.FindProperty("maxStack");
        typeProp = serializedObject.FindProperty("type");
        needIndicatorProp = serializedObject.FindProperty("needIndicator");
        spritePlantGrowsProp = serializedObject.FindProperty("spritePlantGrows");
        harvestPriceProp = serializedObject.FindProperty("harvestPrice");
        attackDamageProp = serializedObject.FindProperty("attackDamage");
        canHarvestMultipleTimesProp = serializedObject.FindProperty("canHarvestMultipleTimes");
        spriteHarvestMultipleTimesProp = serializedObject.FindProperty("spriteHarvestMultipleTimes");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ShowDefaultProps();
        for (var i = 0; i < tagsProp.arraySize; i++)
        {
            switch ((ItemTag)tagsProp.GetArrayElementAtIndex(i).enumValueIndex)
            {
                case ItemTag.Plant:
                    break;
                case ItemTag.Weapon:
                    EditorGUILayout.PropertyField(attackDamageProp);
                    break;
                case ItemTag.Tool:
                    break;
                case ItemTag.Material:
                    break;
                case ItemTag.Seed:
                    EditorGUILayout.PropertyField(spritePlantGrowsProp);
                    EditorGUILayout.PropertyField(canHarvestMultipleTimesProp);
                    if (canHarvestMultipleTimesProp.boolValue)
                    {
                        EditorGUILayout.PropertyField(spriteHarvestMultipleTimesProp);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowDefaultProps()
    {
        EditorGUILayout.PropertyField(tagsProp);
        EditorGUILayout.PropertyField(itemNameProp);
        EditorGUILayout.PropertyField(itemDesProp);
        EditorGUILayout.PropertyField(spriteItemProp);
        EditorGUILayout.PropertyField(maxStackProp);
        EditorGUILayout.PropertyField(typeProp);
        EditorGUILayout.PropertyField(needIndicatorProp);
    }
}