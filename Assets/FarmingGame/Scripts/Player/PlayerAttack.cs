using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Collider2D hit;

    public void Chop()
    {
        hit.gameObject.SetActive(true);
    }

    public void DoneChopping()
    {
        hit.gameObject.SetActive(false);
    }
}