using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject ScoreBoardPrefab;

    private GameObject scoreBoard;
    private TextMeshPro playerScore;
    private TextMeshPro enemyScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Start()
    {
        scoreBoard = Instantiate(ScoreBoardPrefab, Vector2.zero, Quaternion.identity, transform);
        TextMeshPro[] Scores =  scoreBoard.GetComponentsInChildren<TextMeshPro>();

        foreach(TextMeshPro score in Scores)
        {
            if(score.gameObject.name == "PlayerScore")
            {
                playerScore = score;
            }
            else if(score.gameObject.name == "EnemyScore")
            {
                enemyScore = score;
            }
        }
    }

    public void AddPlayerScore(int value)
    {
        int currentScore = int.Parse(playerScore.text);
        currentScore += value;
        playerScore.text = currentScore.ToString();
    }
    public void AddEnemyScore(int value)
    {
        int currentScore = int.Parse(enemyScore.text);
        currentScore += value;
        enemyScore.text = currentScore.ToString();
    }
}
