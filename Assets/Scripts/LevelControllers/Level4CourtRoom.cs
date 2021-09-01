using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4CourtRoom : MonoBehaviour, ILevelController
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
    [SerializeField] private DialougeSequence witnessDeadDialouge = null;
    private readonly string[] phrases = { "FATHER", "GUN", "COURAGE?", "FEAR?", "SCREWED", "FATHER", "GUN", "COURAGE?", "FEAR?", "SCREWED" };


    private bool highlightEvent = false;
    private bool dialougeOn = false;
    private bool wordGameOn = false;
    private bool fading = false;
    private bool objectionOn = false;

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
        uIController.Sherlock(phrases);
        yield return new WaitForSeconds(2);
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
        uIController.HighlighObject(accused, "WHERE IS THE ACCUSED ?");
        highlightEvent = true;
        while (highlightEvent)
            yield return null;
        wordGameController.PlayWordGame(wordGameData1);
        wordGameOn = true;
        while (wordGameOn)
            yield return null;
        wordGameController.TurnOffGame();
        uIController.ObjectionEvent(30, 60, 2);
        objectionOn = true;
        while (objectionOn)
            yield return null;        
        underStudyController.OnWalkTo(courtStandingPoint);
        while (underStudyController.isWalking)
            yield return null;
        uIController.StartConversationWithColor(witnessDeadDialouge.dialouges);
        dialougeOn = true;
        while (dialougeOn)
            yield return null;
        uIController.FadeToBlack();
        fading = true;
        while (fading)
            yield return null;
        SceneManager.LoadScene("Scene6Street");

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

    public void OnObjectionOver()
    {
        objectionOn = false;
    }
}
