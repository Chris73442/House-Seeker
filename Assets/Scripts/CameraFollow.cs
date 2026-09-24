using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float offsetX = 2f;
    public float offsetY = 1f; 

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(
                player.position.x + offsetX,
                player.position.y + offsetY,
                transform.position.z
            );
        }
    }
}