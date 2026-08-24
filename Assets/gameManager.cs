using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static int PlayerScore1 = 0; // Pontuação do player 1
    public static int PlayerScore2 = 0; // Pontuação do player 2

    public GUISkin layout;              // Skin customizado (fonte, estilos, etc)
    GameObject theBall;                 // Referência ao objeto bola

    GUIStyle estiloCentralizado;        // Estilo pro placar
    GUIStyle estiloVencedor;            // Estilo pro texto de vitória
    bool jogoFinalizado = false;        // Evita chamar ResetBall todo frame

    public AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball"); // Busca a referência da bola
        source = GetComponent<AudioSource>();
    }

    public static void Score(string wallID)
    {
        if (wallID == "gol")
        {
            PlayerScore1++;
        }
        else if (wallID == "gol2")
        {
            PlayerScore2++;
        }
    }

    // Gerência da pontuação e fluxo do jogo
    void OnGUI()
    {
        if (layout == null)
        {
            Debug.LogError("GUISkin não atribuído no gameManager!");
            return;
        }

        GUI.skin = layout;

        // Cria os estilos uma vez (evita recriar objeto todo frame)
        if (estiloCentralizado == null)
        {
            estiloCentralizado = new GUIStyle(GUI.skin.label);
            estiloCentralizado.alignment = TextAnchor.MiddleCenter;
        }

        if (estiloVencedor == null)
        {
            estiloVencedor = new GUIStyle(GUI.skin.label);
            estiloVencedor.alignment = TextAnchor.MiddleCenter;
            estiloVencedor.fontSize = 40;
        }

        // Placar
        GUI.Label(new Rect(Screen.width / 2 - 170 - 12, 20, 100, 100), "" + PlayerScore1, estiloCentralizado);
        GUI.Label(new Rect(Screen.width / 2 + 170 + 12, 20, 100, 100), "" + PlayerScore2, estiloCentralizado);

        // Botão de restart (maior, pra não cortar o texto)
        if (GUI.Button(new Rect(Screen.width / 2 - 100, 35, 300, 60), "RESTART"))
        {
            PlayerScore1 = 0;
            PlayerScore2 = 0;
            jogoFinalizado = false;
            theBall.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }

        // Condições de vitória
        if (PlayerScore1 == 3)
        {
            GUI.Label(new Rect(Screen.width / 2 - 300, 200, 600, 100), "PLAYER ONE WINS", estiloVencedor);
            source.Play();

            if (!jogoFinalizado)
            {
                theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
                jogoFinalizado = true;
            }
        }
        else if (PlayerScore2 == 3)
        {
            GUI.Label(new Rect(Screen.width / 2 - 300, 200, 600, 100), "PLAYER TWO WINS", estiloVencedor);
            source.Play();

            if (!jogoFinalizado)
            {
                theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
                jogoFinalizado = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}