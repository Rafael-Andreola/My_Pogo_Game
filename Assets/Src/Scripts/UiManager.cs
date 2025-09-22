using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] float duration; // tempo para fazer o fade
    [SerializeField] float durationAlpha100; // tempo para fazer o fade
    [SerializeField] float targetAlpha;
    [SerializeField] List<UIElement> UiElements;

    Dictionary<UIIdentifier, GameObject> uiDictionary = new Dictionary<UIIdentifier, GameObject>();
    TextMeshProUGUI leftScore;
    TextMeshProUGUI rightScore;
    GameObject winBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Start()
    {
        PopulateDictionary();
    }

    public void ShowInGameUI()
    {
        HideAllUI();

        ShowUI(UIIdentifier.ScoreBoard);

        GameObject scoreBoard = GetUIElement(UIIdentifier.ScoreBoard);

        TextMeshProUGUI[] Scores = scoreBoard.GetComponentsInChildren<TextMeshProUGUI>();

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

    private GameObject GetUIElement(UIIdentifier identifier)
    {
        return uiDictionary.GetValueOrDefault(identifier);
    }

    private void ShowUI(UIIdentifier identifier)
    {
        GameObject uiObject = GetUIElement(identifier);
        if (uiObject != null)
        {
            uiObject.SetActive(true);
        }
    }

    private void HideAllUI()
    {
        foreach (var ui in uiDictionary)
        {
            ui.Value.SetActive(false);
        }
    }

    private void PopulateDictionary()
    {
        foreach (UIElement element in UiElements)
        {
            uiDictionary.Add(element.uiIdentifier, element.uiObject);
        }
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

    public void AddScore(PlayerSide side, int value = 1)
    {
        TextMeshProUGUI scoreText = null;

        if (side == PlayerSide.Left)
        {
            scoreText = leftScore;
        }
        else if (side == PlayerSide.Right)
        {
            scoreText = rightScore;
        }

        if (scoreText != null)
        {
            int currentScore = int.Parse(scoreText.text);
            currentScore += value;
            scoreText.text = currentScore.ToString();
            StartCoroutine(StartNFadeText(scoreText));
        }
    }

    public int GetScore(PlayerSide side) => side switch
    {
        PlayerSide.Left => int.Parse(leftScore.text),
        PlayerSide.Right => int.Parse(rightScore.text),
        _ => 0
    };

    public void ShowWinText(PlayerSide side)
    {
        // winBox = Instantiate(WinBoxPrefab, Vector2.zero, Quaternion.identity);

        // TextMeshProUGUI winText = winBox.GetComponentInChildren<TextMeshProUGUI>();

        // if (winText != null)
        // {
        //     winText.text = side == PlayerSide.Left ? "Left Player Wins!" : "Right Player Wins!";
        //     winBox.GetComponentInChildren<Button>().onClick.AddListener(() =>
        // {
        //     FindFirstObjectByType<GameManager>().ResetGame();
        // });
        // }
    }

    public void ResetGameUI()
    {
        if (winBox != null)
        {
            Destroy(winBox);
            winBox = null;
        }

        leftScore.text = "0";
        rightScore.text = "0";
    }
}
