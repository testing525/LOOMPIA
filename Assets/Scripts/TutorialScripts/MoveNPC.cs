using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 2f;

    void Update()
    {
        if (GameSpeed.Instance == null) return;

        float moveAmount = baseSpeed * GameSpeed.Instance.GetMultiplier() * Time.deltaTime;
        transform.Translate(Vector3.left * moveAmount);
    }
}
