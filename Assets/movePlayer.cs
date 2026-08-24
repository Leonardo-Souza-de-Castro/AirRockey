using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public float speed = 50f;
    public float boundY = 4.4f;            // Define os limites em Y
    public float minboundY = 0.5f;            // Define os limites em Y
    public float boundX = 2.25f;            // Define os limites em X
    private Rigidbody2D rb2d;
    public AudioSource source;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D (Collision2D coll) {
        source.Play();
    }


    void Update()
    {
        Vector3 playerPos = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(mousePos.y > boundY)
        {
            mousePos.y = boundY;
        }
        else if(mousePos.y < minboundY)
        {
            mousePos.y = minboundY;
        }

        if(mousePos.x > boundX)
        {
            mousePos.x = boundX;
        }
        else if(mousePos.x < -boundX)
        {
            mousePos.x = -boundX;
        }

        Vector3 dir = mousePos - playerPos;
        dir.Normalize();

        Vector3 speedVec = dir * speed;

        var vel = rb2d.linearVelocity;
        vel.x = speedVec.x;
        vel.y = speedVec.y;
        rb2d.linearVelocity = vel; 

    }
}