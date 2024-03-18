using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerViewModel", menuName = "ViewModel/PlayerViewModel")]
public class PlayerViewModel : ScriptableSingleton<PlayerViewModel>
{
    public Transform PlayerTransform { get; private set; }

    public void UpdateTransform(Transform transform)
    {
        PlayerTransform = transform;
    }
}