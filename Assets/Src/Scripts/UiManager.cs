using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject ScoreBoardPrefab;
    [SerializeField] float duration; // tempo para fazer o fade
    [SerializeField] float durationAlpha100; // tempo para fazer o fade
    [SerializeField] float targetAlpha;

    private GameObject scoreBoard;
    private TextMeshProUGUI leftScore;
    private TextMeshProUGUI rightScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Start()
    {
        scoreBoard = Instantiate(ScoreBoardPrefab, Vector2.zero, Quaternion.identity);
        TextMeshProUGUI[] Scores = scoreBoard.GetComponentsInChildren<TextMeshProUGUI>();

        Debug.Log(Scores.Length);

        foreach (TextMeshProUGUI score in Scores)
        {
            if (score.gameObject.name == "LeftScore")
            {
                leftScore = score;
            }
            else if (score.gameObject.name == "RightScore")
            {
                rightScore = score;
            }
        }

        StartCoroutine(StartNFadeText(leftScore));
        StartCoroutine(StartNFadeText(rightScore));
    }

    IEnumerator StartNFadeText(TextMeshProUGUI text)
    {
        Color originalColor = text.color;
        // Garantir que começa totalmente opaco
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return new WaitForSeconds(durationAlpha100);

        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            float newAlpha = Mathf.Lerp(1f, targetAlpha, t);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            yield return null;
        }

        // Garantir que chega no alpha final
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }

    public void AddLeftScore(int value = 1)
    {
        int currentScore = int.Parse(leftScore.text);
        currentScore += value;
        leftScore.text = currentScore.ToString();
        StartCoroutine(StartNFadeText(leftScore));
    }

    public void AddRightScore(int value = 1)
    {
        int currentScore = int.Parse(rightScore.text);
        currentScore += value;
        rightScore.text = currentScore.ToString();
        StartCoroutine(StartNFadeText(rightScore));
    }
}
