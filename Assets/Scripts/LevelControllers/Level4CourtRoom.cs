using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4CourtRoom : MonoBehaviour, ILevelController
{
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private NPCController underStudyController = null;
    [SerializeField] private WordGameController wordGameController = null;
    [SerializeField] private GameObject client = null;
    [SerializeField] private GameObject accused = null;
    [SerializeField] private DialougeSequence witnessDeadDialouge = null;
    [SerializeField] private Transform courtStandingPoint = null;
    [SerializeField] private UIController uIController = null;
    [SerializeField] private GameObject file = null;
    [SerializeField] private WordGameData wordGameData1 = null;


    private bool highlightEvent = false;
    private bool wordGameOn = false;

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
        uIController.HighlighObject(file, "WHERE IS YOUR FILE ?");
        highlightEvent = true;
        while (highlightEvent)
            yield return null;
        Debug.Log("HighlightOver");
        underStudyController.OnWalkTo(courtStandingPoint);
        while (underStudyController.isWalking)
            yield return null;
        uIController.HighlighObject(client, "WHERE IS YOUR CLIENT ?");
        highlightEvent = true;
        while (highlightEvent)
            yield return null;
        uIController.HighlighObject(accused, "WHERE IS THE DEFENDANT ?");
        highlightEvent = true;
        while (highlightEvent)
            yield return null;
        wordGameController.PlayWordGame(wordGameData1);
        wordGameOn = true;
        while (wordGameOn)
            yield return null;

        //uIController.StartConversation(witnessDeadDialouge.dialouges);        

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
}
