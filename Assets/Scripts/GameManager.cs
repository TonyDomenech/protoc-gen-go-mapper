using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text leftScoreText;
    public Text rightScoreText;

    [Header("Puntuación")]
    public int winningScore = 5;
    public Ball ball;

    [Header("Modo de Juego")]
    public bool vsAI = false;
    [Range(1,5)]
    public int difficulty = 3;
    public AIPaddleController aiPaddle;

    private int leftScore;
    private int rightScore;
    private bool isPaused;
    public bool IsGameOver { get; private set; }

    void Start()
    {
        Time.timeScale = 1f;
        leftScore = rightScore = 0;
        if (leftScoreText) leftScoreText.text = "0";
        if (rightScoreText) rightScoreText.text = "0";
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
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            IsGameOver = false;
            isPaused = false;
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
            if (rightScoreText) rightScoreText.text = rightScore.ToString();
            if (rightScore >= winningScore) GameOver();
        }
        else
        {
            leftScore++;
            if (leftScoreText) leftScoreText.text = leftScore.ToString();
            if (leftScore >= winningScore) GameOver();
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

    void Update()
    {
        if (IsGameOver) return;
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        if (ball != null)
        {
            ball.Stop();
        }
        Debug.Log("Game Over");
    }
}
