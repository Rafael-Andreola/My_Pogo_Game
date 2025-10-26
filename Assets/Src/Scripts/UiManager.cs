using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] float duration; // tempo para fazer o fade
    [SerializeField] float durationAlpha100; // duração do alpha 100
    [SerializeField] float targetAlpha; // alpha alvo (0 = invisível, 1 = totalmente visível)
    [SerializeField] List<UIElement> UiElements; // Lista de elementos UI para popular o dicionário

    Dictionary<UIIdentifier, GameObject> uiDictionary;
    TextMeshProUGUI leftScore;
    TextMeshProUGUI rightScore;
    GameObject winBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Awake()
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
            if (score.name == "LeftScore")
            {
                leftScore = score;
            }
            else if (score.name == "RightScore")
            {
                rightScore = score;
            }
        }

        ResetGameUI();

        StartCoroutine(UI_Utils.StartNFadeText(leftScore, duration, durationAlpha100, targetAlpha));
        StartCoroutine(UI_Utils.StartNFadeText(rightScore, duration, durationAlpha100, targetAlpha));
    }

    private GameObject GetUIElement(UIIdentifier identifier)
    {
        if (uiDictionary == null || uiDictionary.Count == 0)
        {
            Debug.Log("Dicionário de UI nulo ou vazio");
            return null;
        }

        if (uiDictionary.TryGetValue(identifier, out GameObject uiElement) == false)
        {
            Debug.LogWarning($"UI com identificador {identifier} não encontrada no dicionário.");
            return null;
        }

        return uiElement;
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
        uiDictionary = new Dictionary<UIIdentifier, GameObject>();

        foreach (UIElement element in UiElements)
        {
            uiDictionary.Add(element.uiIdentifier, element.uiObject);
        }
    }

    public void AddScore(PlayerSide side, int value = 1)
    {
        int currentScore = GetScore(side);
        currentScore += value;

        TextMeshProUGUI scoreText = side == PlayerSide.Left ? leftScore : rightScore;
        scoreText.text = currentScore.ToString();
        StartCoroutine(UI_Utils.StartNFadeText(scoreText, duration, durationAlpha100, targetAlpha));
    }

    public int GetScore(PlayerSide side) => side switch
    {
        PlayerSide.Left => int.Parse(leftScore.text),
        PlayerSide.Right => int.Parse(rightScore.text),
        _ => 0
    };

    public void ShowWinText(PlayerSide side)
    {
        GameObject winBox = GetUIElement(UIIdentifier.WinBox);

        TextMeshProUGUI winText = winBox.GetComponentInChildren<TextMeshProUGUI>();
        winBox.SetActive(true);

        if (winText != null)
        {
            winText.text = side == PlayerSide.Left ? "Left Player Wins!" : "Right Player Wins!";
            winBox.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                FindFirstObjectByType<GameManager>().ResetMatch();
            });
        }
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
