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

    private int leftScore;
    private int rightScore;

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
    }
}
