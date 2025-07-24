using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text leftScoreText;
    public Text rightScoreText;
    public Text gameOverText;

    [Header("Modo de Juego")]
    public bool vsAI = false;
    [Range(1,5)]
    public int difficulty = 3;
    public AIPaddleController aiPaddle;

    public int scoreToWin = 5;

    private int leftScore;
    private int rightScore;
    private bool gameOver = false;
    private bool paused = false;

    void Start()
    {
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

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);

        leftScoreText.text = "0";
        rightScoreText.text = "0";
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void TogglePause()
    {
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
    }

    public void Restart()
    {
        leftScore = 0;
        rightScore = 0;
        leftScoreText.text = "0";
        rightScoreText.text = "0";
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
        gameOver = false;
        paused = false;
        Time.timeScale = 1f;

        Ball ball = FindObjectOfType<Ball>();
        if (ball != null)
        {
            ball.ResetAndLaunch();
        }
    }

    public void Score(string goalName)
    {
        if (gameOver)
            return;

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

        if (leftScore >= scoreToWin || rightScore >= scoreToWin)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver = true;
        if (gameOverText != null)
        {
            string winner = leftScore > rightScore ? "\u00a1Jugador 1 gana!" : "\u00a1Jugador 2 gana!";
            gameOverText.text = winner + " Pulsa R para reiniciar";
            gameOverText.gameObject.SetActive(true);
        }
        Time.timeScale = 0f;
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
