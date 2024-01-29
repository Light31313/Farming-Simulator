using Sirenix.OdinInspector;
using UnityEngine;

public class DevTool : MonoBehaviour
{
    [Button]
    public void GoToSleep()
    {
        TimeSignals.OnGoToSleep.Dispatch();
    }
}