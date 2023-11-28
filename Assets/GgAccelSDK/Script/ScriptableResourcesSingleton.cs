using UnityEngine;

public abstract class ScriptableResourcesSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T _Instance;
    private static bool _IsInitialized;

    public static T GetInstance()
    {
        if (!_IsInitialized)
        {
            _Instance = DataHolder.GetInstance().GetData<T>();
            _IsInitialized = true;
        }

        return _Instance;
    }
}