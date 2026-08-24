using UnityEngine;

public class moveIAPlayer : MonoBehaviour
{
    public Transform disco;              // Arraste o disco (disco) aqui
    public float speed = 5f;
    public float boundY = -4.4f;            // Define os limites em Y
    public float minboundY = -0.5f;            // Define os limites em Y
    public float boundX = 2.25f;            // Define os limites em X
    public AudioSource source;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();

        // Pega a largura interna do campo a partir do collider do retângulo verde
        float raioPlayer = GetComponent<CircleCollider2D>()?.radius ?? 0f;
    }

    void OnCollisionEnter2D (Collision2D coll) {
        source.Play();
    }


    void Update()
    {
        //se não houver disco não faz nada
        if (disco == null) return;

        //fica em uma posição de descanso, se o disco estiver na metade do campo do player

        //movimenta o player em direção ao alvo (descanso ou disco)

        //limita o movimento do player dentro do campo
        if(rb.position.y < boundY){
            rb.position = new Vector2(rb.position.x, boundY);
        }
        else if(rb.position.y > minboundY){
            rb.position = new Vector2(rb.position.x, minboundY);
        }

        if(rb.position.x < -boundX){
            rb.position = new Vector2(-boundX, rb.position.y);
        }
        else if(rb.position.x > boundX){
            rb.position = new Vector2(boundX, rb.position.y);
        }

        if(disco.position.y > minboundY){
            Vector2 alvo = new Vector2(disco.position.x, -3);
            float distancia1 = Vector2.Distance(rb.position, alvo);

            if(distancia1 > 0.1f){
                Vector2 direction1 = (alvo - rb.position).normalized;
                rb.linearVelocity = direction1 * speed;
            } else {
                rb.linearVelocity = Vector2.zero;
            }
        }else{
            //movimenta o player em direção ao disco
            Vector2 direction = (disco.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }

        
    }
}