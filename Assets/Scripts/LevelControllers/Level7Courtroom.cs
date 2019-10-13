using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level7Courtroom : MonoBehaviour, ILevelController
{
    [Header("Controllers")]
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private NPCController underStudyController = null;
    [SerializeField] private WordGameController wordGameController = null;


    [SerializeField] private GameObject client = null;
    [SerializeField] private GameObject accused = null;

    [SerializeField] private Transform courtStandingPoint = null;
    [SerializeField] private UIController uIController = null;
    [SerializeField] private GameObject file = null;
    [SerializeField] private WordGameData wordGameData1 = null;

    [Header("Dialouges")]
    [SerializeField] private DialougeSequence firstDialouge = null;
    [SerializeField] private DialougeSequence winnerdialouge1 = null;
    [SerializeField] private DialougeSequence winnderdialouge2 = null;
    private readonly string[] phrases = { "FATHER", "GUN", "COURAGE?", "FEAR?", "SCREWED", "FATHER", "GUN", "COURAGE?", "FEAR?", "SCREWED" };


    private bool highlightEvent = false;
    private bool dialougeOn = false;
    private bool wordGameOn = false;
    private bool fading = false;

    private void Awake()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        StartCoroutine(LevelCoroutine());
    }

    IEnumerator LevelCoroutine()
    {
        yield return new WaitForSeconds(2);
        wordGameController.allWin = true;
        uIController.StartConversationWithColor(firstDialouge.dialouges);
        dialougeOn = true;
        while (dialougeOn)
            yield return null;
        uIController.HighlighObject(file, "WHERE IS YOUR FILE ?");
        highlightEvent = true;
        while (highlightEvent)
            yield return null;
        uIController.HighlighObject(client, "WHERE IS YOUR CLIENT ?");
        highlightEvent = true;
        while (highlightEvent)
            yield return null;
        uIController.HighlighObject(accused, "WHERE IS THE ACCUSSED ?");
        highlightEvent = true;
        while (highlightEvent)
            yield return null;
        wordGameController.PlayWordGame(wordGameData1);
        wordGameOn = true;
        while (wordGameOn)
            yield return null;
        wordGameController.TurnOffGame();
        uIController.StartConversationWithColor(winnerdialouge1.dialouges);
        dialougeOn=true;
        while(dialougeOn)
            yield return null;        
        underStudyController.OnWalkTo(courtStandingPoint);
        while (underStudyController.isWalking)
            yield return null;
        uIController.StartConversationWithColor(winnderdialouge2.dialouges);
        dialougeOn = true;
        while (dialougeOn)
            yield return null;

        uIController.Sherlock(phrases);
        uIController.FadeToBlack();
        fading = true;
        while (fading)
            yield return null;
        SceneManager.LoadScene("End");

        Debug.Log("Done");
        yield return null;
    }

    public void OnHighlightOver()
    {
        highlightEvent = false;
    }

    public void OnWordGameOver()
    {
        wordGameOn = false;
    }

    public void OnDialougeOver()
    {
        dialougeOn = false;
    }

    public void OnFadeOver()
    {
        fading = false;
    }
}
