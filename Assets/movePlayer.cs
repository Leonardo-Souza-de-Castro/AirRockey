using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public Collider2D leftWall;
    public Collider2D rightWall;
    public Collider2D topWall;
    public Collider2D bottomWall;

    private Collider2D playerCollider;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 pos = mousePos;

        // Limites das paredes
        float left = leftWall.bounds.max.x;
        float right = rightWall.bounds.min.x;
        float bottom = bottomWall.bounds.max.y;
        float top = topWall.bounds.min.y;

        // Metade do tamanho do player
        float halfWidth = playerCollider.bounds.extents.x;
        float halfHeight = playerCollider.bounds.extents.y;

        // Impede o player de atravessar as paredes
        pos.x = Mathf.Clamp(
            pos.x,
            left + halfWidth,
            right - halfWidth
        );

        pos.y = Mathf.Clamp(
            pos.y,
            bottom + halfHeight,
            top - halfHeight
        );

        pos.z = transform.position.z;

        transform.position = pos;
    }
}