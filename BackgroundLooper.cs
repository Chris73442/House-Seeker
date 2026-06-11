using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public Transform cam;
    public Transform bg1;
    public Transform bg2;

    private float width;
    private float height;

    void Start()
    {
        SpriteRenderer sr = bg1.GetComponent<SpriteRenderer>();
        width = sr.bounds.size.x;
        height = sr.bounds.size.y;
    }

    void Update()
    {
        // ===== HORIZONTAL =====
        if (cam.position.x - bg1.position.x > width)
        {
            MoveRight(bg1, bg2);
        }

        if (cam.position.x - bg2.position.x > width)
        {
            MoveRight(bg2, bg1);
        }

        // kiri (tambahan biar lebih fleksibel)
        if (cam.position.x - bg1.position.x < -width)
        {
            MoveLeft(bg1, bg2);
        }

        if (cam.position.x - bg2.position.x < -width)
        {
            MoveLeft(bg2, bg1);
        }

        // ===== VERTICAL =====
        if (cam.position.y - bg1.position.y > height)
        {
            MoveUp(bg1, bg2);
        }

        if (cam.position.y - bg2.position.y > height)
        {
            MoveUp(bg2, bg1);
        }

        if (cam.position.y - bg1.position.y < -height)
        {
            MoveDown(bg1, bg2);
        }

        if (cam.position.y - bg2.position.y < -height)
        {
            MoveDown(bg2, bg1);
        }
    }

    void MoveRight(Transform from, Transform to)
    {
        from.position = new Vector3(
            to.position.x + width,
            from.position.y,
            from.position.z
        );
    }

    void MoveLeft(Transform from, Transform to)
    {
        from.position = new Vector3(
            to.position.x - width,
            from.position.y,
            from.position.z
        );
    }

    void MoveUp(Transform from, Transform to)
    {
        from.position = new Vector3(
            from.position.x,
            to.position.y + height,
            from.position.z
        );
    }

    void MoveDown(Transform from, Transform to)
    {
        from.position = new Vector3(
            from.position.x,
            to.position.y - height,
            from.position.z
        );
    }
}