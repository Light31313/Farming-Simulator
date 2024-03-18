using System;
using Cinemachine.Editor;
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
        harvestedPlantProp,
        harvestPriceProp,
        attackDamageProp,
        canHarvestMultipleTimesProp,
        spriteHarvestMultipleTimesProp,
        dayToGrowEachStateProp,
        dayToRegrowProp,
        minHarvestQuantityProp,
        maxHarvestQuantityProp;

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
        harvestedPlantProp = serializedObject.FindProperty("harvestedPlant");
        harvestPriceProp = serializedObject.FindProperty("harvestPrice");
        attackDamageProp = serializedObject.FindProperty("attackDamage");
        canHarvestMultipleTimesProp = serializedObject.FindProperty("canHarvestMultipleTimes");
        spriteHarvestMultipleTimesProp = serializedObject.FindProperty("spriteHarvestMultipleTimes");
        dayToGrowEachStateProp = serializedObject.FindProperty("dayToGrowEachState");
        dayToRegrowProp = serializedObject.FindProperty("dayToRegrow");
        minHarvestQuantityProp = serializedObject.FindProperty("minHarvestQuantity");
        maxHarvestQuantityProp = serializedObject.FindProperty("maxHarvestQuantity");
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
                    EditorGUILayout.PropertyField(needIndicatorProp);
                    EditorGUILayout.PropertyField(attackDamageProp);
                    break;
                case ItemTag.Tool:
                    EditorGUILayout.PropertyField(needIndicatorProp);
                    break;
                case ItemTag.Material:
                    break;
                case ItemTag.Seed:
                    EditorGUILayout.PropertyField(spritePlantGrowsProp);
                    EditorGUILayout.PropertyField(harvestedPlantProp);
                    EditorGUILayout.PropertyField(dayToGrowEachStateProp);
                    EditorGUILayout.PropertyField(minHarvestQuantityProp);
                    EditorGUILayout.PropertyField(maxHarvestQuantityProp);
                    EditorGUILayout.Space(10);
                    EditorGUILayout.PropertyField(canHarvestMultipleTimesProp);

                    if (canHarvestMultipleTimesProp.boolValue)
                    {
                        EditorGUILayout.PropertyField(spriteHarvestMultipleTimesProp);
                        EditorGUILayout.PropertyField(dayToRegrowProp);
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
    }
}