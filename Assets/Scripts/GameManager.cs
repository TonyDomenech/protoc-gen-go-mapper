using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text leftScoreText;
    public Text rightScoreText;

    [Header("Modo de Juego")]
    public bool vsAI = false;
    [Range(1,5)]
    public int difficulty = 3;
    public AIPaddleController aiPaddle;

    [Header("Partida")]
    public int winningScore = 5;
    public Text winText;

    public bool IsPaused { get; private set; }
    public bool GameEnded { get; private set; }

    private Ball ball;

    private int leftScore;
    private int rightScore;

    void Start()
    {
        ball = FindObjectOfType<Ball>();

        if (aiPaddle != null)
        {
            PaddleController playerControl = aiPaddle.GetComponent<PaddleController>();
            aiPaddle.enabled = vsAI;
            if (playerControl)
                playerControl.enabled = !vsAI;

            if (vsAI)
            {
                aiPaddle.difficulty = Mathf.Clamp(difficulty, 1, 5);
            }
        }

        if (winText != null)
        {
            winText.text = "";
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }

        if (GameEnded && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void Score(string goalName)
    {
        if (goalName == "LeftGoal")
        {
            rightScore++;
            rightScoreText.text = rightScore.ToString();
        }
        else
        {
            leftScore++;
            leftScoreText.text = leftScore.ToString();
        }

        if (leftScore >= winningScore)
        {
            EndGame("Jugador 1 gana!");
        }
        else if (rightScore >= winningScore)
        {
            EndGame("Jugador 2 gana!");
        }
    }

    // Returns the ball speed depending on the selected difficulty.
    public float GetBallSpeed(float defaultSpeed)
    {
        switch (Mathf.Clamp(difficulty, 1, 5))
        {
            case 1: // Facil
                return 2f;
            case 2: // Medio
                return 2.5f;
            case 3: // Avanzado
                return 3f;
            case 4: // Dificil
                return 3.5f;
            case 5: // Imposible
                return 4f;
        }
        return defaultSpeed;
    }

    void EndGame(string message)
    {
        GameEnded = true;
        if (winText != null)
            winText.text = message + "\nPulsa R para reiniciar";
        if (ball != null)
            ball.StopMovement();
    }

    void TogglePause()
    {
        if (GameEnded) return;

        IsPaused = !IsPaused;
        if (ball != null)
            ball.SetPaused(IsPaused);
    }

    void RestartGame()
    {
        leftScore = rightScore = 0;
        leftScoreText.text = "0";
        rightScoreText.text = "0";
        GameEnded = false;
        IsPaused = false;
        if (winText != null)
            winText.text = "";
        if (ball != null)
        {
            ball.ResetAndLaunch();
            ball.SetPaused(false);
        }
    }
}
