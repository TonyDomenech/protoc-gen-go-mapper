using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    PlayerVsPlayer,
    PlayerVsAI,
    AIVsAI
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text leftScoreText;
    public Text rightScoreText;
    public Text winText;
    public Ball ball;

    [Header("Puntuación")]
    public int scoreLimit = 5;

    [Header("Modo de Juego")]
    public GameMode gameMode = GameMode.PlayerVsPlayer;
    [Range(1,5)]
    public int difficulty = 3;
    public AIPaddleController leftAI;
    public PaddleController leftPlayer;
    public AIPaddleController rightAI;
    public PaddleController rightPlayer;

    private int leftScore;
    private int rightScore;
    private bool gameOver;

    public bool IsGameOver => gameOver;

    void Start()
    {
        if (leftAI != null && leftPlayer != null)
        {
            bool leftIsAI = gameMode == GameMode.AIVsAI;
            leftAI.enabled = leftIsAI;
            leftPlayer.enabled = !leftIsAI;
            if (leftIsAI)
                leftAI.difficulty = Mathf.Clamp(difficulty, 1, 5);
        }

        if (rightAI != null && rightPlayer != null)
        {
            bool rightIsAI = gameMode != GameMode.PlayerVsPlayer;
            rightAI.enabled = rightIsAI;
            rightPlayer.enabled = !rightIsAI;
            if (rightIsAI)
                rightAI.difficulty = Mathf.Clamp(difficulty, 1, 5);
        }

        leftScore = rightScore = 0;
        gameOver = false;
        if (winText != null)
            winText.gameObject.SetActive(false);
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

        if (leftScore >= scoreLimit || rightScore >= scoreLimit)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameOver = true;
        if (ball != null)
            ball.Stop();
        if (winText != null)
        {
            winText.text = leftScore > rightScore ? "Gana Azul" : "Gana Rojo";
            winText.gameObject.SetActive(true);
        }
    }

    public void ResetGame()
    {
        leftScore = rightScore = 0;
        leftScoreText.text = "0";
        rightScoreText.text = "0";
        gameOver = false;
        if (winText != null)
            winText.gameObject.SetActive(false);
        if (ball != null)
            ball.ResetAndLaunch();
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
}
