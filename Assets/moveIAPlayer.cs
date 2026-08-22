using UnityEngine;

public class moveIAPlayer : MonoBehaviour
{
    public Transform puck;          // Arraste o disco aqui
    public float speed = 5f;        // Velocidade da IA
    public float minX, maxX;        // Limites no eixo X (largura do gol)
    public float fixedZ;            // Posição fixa no eixo Z (lado da IA)

    void Update()
    {
        if (puck == null) return;

        // Pega a posição X do disco e trava o Z no lado da IA
        Vector3 targetPos = new Vector3(puck.position.x, transform.position.y, fixedZ);

        // Limita o movimento para não sair da área permitida
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);

        // Move suavemente até a posição alvo
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }


}
