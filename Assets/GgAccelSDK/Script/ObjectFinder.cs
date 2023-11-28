using System;
using System.Collections.Generic;
using GgAccel;
using UnityEngine;

public class ObjectFinder : MonoSingleton<ObjectFinder>
{
    [SerializeField] private List<ObjectType> objects = new List<ObjectType>();
    public DataHolder dataHolder;

    public static Transform GetObject(ObjectID t)
    {
        foreach (var o in Instance.objects)
        {
            if (o.type == t)
            {
                return o.transform;
            }
        }

        return null;
    }
}

[Serializable]
public class ObjectType
{
    public ObjectID type;
    public Transform transform;
}

public enum ObjectID
{
    PopupHolder
}