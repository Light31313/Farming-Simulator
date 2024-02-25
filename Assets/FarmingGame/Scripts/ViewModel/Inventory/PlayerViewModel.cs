using UnityEngine;

[CreateAssetMenu(fileName = "PlayerViewModel", menuName = "ViewModel/PlayerViewModel")]
public class PlayerViewModel : ScriptableObject
{
    public Transform PlayerTransform { get; private set; }

    public void UpdateTransform(Transform transform)
    {
        PlayerTransform = transform;
    }
}