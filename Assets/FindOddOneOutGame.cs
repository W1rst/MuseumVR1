using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindOddOneOutGame : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject panel;
    public TextMeshProUGUI qtext;
    public Button yesButton;
    public Button noButton;
    public Button finishButton;
    public Button playButton;
    public TextMeshProUGUI resultText;

    private List<int> remainingIndexes = new List<int>();
    private int currentCharacterIndex = 0;
    private int correctGuesses = 0;

    void Start()
    {
        finishButton.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);

        finishButton.onClick.AddListener(FinishButtonClick);
    }

    public void StartGame()
    {
        currentCharacterIndex = 0;
        correctGuesses = 0;
        for (int i = 0; i < characters.Length; i++)
        {
            remainingIndexes.Add(i);
        }

        ShowNextCharacter();
        playButton.gameObject.SetActive(false);
    }

    void ShowNextCharacter()
    {
        if (remainingIndexes.Count > 0)
        {
            int randomIndex = Random.Range(0, remainingIndexes.Count);
            currentCharacterIndex = remainingIndexes[randomIndex];

            remainingIndexes.RemoveAt(randomIndex);

            // Показываем персонажа с выбранным индексом
            characters[currentCharacterIndex].SetActive(true);

            panel.SetActive(true);
            qtext.text = "Чи підходить цей персонаж?";

            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(YesButtonClick);
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(NoButtonClick);

            finishButton.gameObject.SetActive(false);
            resultText.gameObject.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            qtext.text = "";
            resultText.text = correctGuesses + "/4";
            finishButton.gameObject.SetActive(true);
            resultText.gameObject.SetActive(true);
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
        }
    }

    void YesButtonClick()
    {
        string characterName = characters[currentCharacterIndex].name;

        if (characterName == "1")
        {
            characters[currentCharacterIndex].SetActive(false);
            correctGuesses++;
        }
        else
        {
            characters[currentCharacterIndex].SetActive(false);
        }
        ShowNextCharacter();
    }

    void NoButtonClick()
    {
        string characterName = characters[currentCharacterIndex].name;

        if (characterName == "2")
        {
            characters[currentCharacterIndex].SetActive(false);
        }
        else
        {
            characters[currentCharacterIndex].SetActive(false);
        }

        ShowNextCharacter();
    }

    public void FinishButtonClick()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i].gameObject.SetActive(false);
        }

        finishButton.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);

        StartGame();
    }
}
