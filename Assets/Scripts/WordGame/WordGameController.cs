using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Events;

public class WordGameController : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel = null;
    [SerializeField] private Text gameText = null;
    [SerializeField] private Button[] buttonList = null;
    [SerializeField] private DataRowPopulator buttonRow = null;
    [SerializeField] private Color rightColor = Color.green;
    [SerializeField] private Color wrongColor = Color.red;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Core.Events.Event onGameComplete = null;


    private int currentAnswerIndex = 0;
    private bool awaitingInput = false;


    public void PlayWordGame(WordGameData wordGameData)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].interactable = false;
        }
        StartCoroutine(WordGameCoroutine(wordGameData));
    }

    public void TurnOffGame()
    {
        mainPanel.SetActive(false);
    }

    IEnumerator WordGameCoroutine(WordGameData wordGameData)
    {
        for (int i = 0; i < wordGameData.wordGameRows.Length; i++)
        {
            string[] buttonRowString = { wordGameData.wordGameRows[i].words[0], wordGameData.wordGameRows[i].words[1], wordGameData.wordGameRows[i].words[2] };
            buttonRow.SetRow(buttonRowString);
            for (int j = 0; j < buttonList.Length; j++)
            {
                buttonList[j].GetComponent<Image>().color = normalColor;
                buttonList[j].interactable = true;
            }
            mainPanel.SetActive(true);
            currentAnswerIndex = wordGameData.wordGameRows[i].correctOption;
            awaitingInput = true;
            while (awaitingInput)
                yield return null;
            yield return new WaitForSeconds(2);

        }
        onGameComplete?.Raise();
        yield return null;
    }

    public void ProcessAnswer(int answer)
    {

        for (int i = 0; i < buttonList.Length; i++)
        {
            if (i == currentAnswerIndex)
                buttonList[i].GetComponent<Image>().color = rightColor;
            else
                buttonList[i].GetComponent<Image>().color = wrongColor;
            buttonList[i].interactable = false;
        }
        awaitingInput = false;
    }
}
