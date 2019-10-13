using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5OpenWorld : MonoBehaviour, ILevelController
{
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private CutSceneInstance firstCutscene = null;
    [SerializeField] private NPCController[] npcRunners = null;
    [SerializeField] private Transform[] runPoints = null;
    [SerializeField] private NPCController shopKeeper = null;
    [SerializeField] private UIController uiController = null;
    [SerializeField] private DialougeSequence firstBadGuyConversation = null;
    [SerializeField] private DialougeSequence secondBadGuyConversation = null;
    [SerializeField] private GameObject gunInHand = null;
    [SerializeField] private GameObject[] objectsToTurnOff = null;
    [SerializeField] private GameObject afterCutscene = null;

    private bool dialougeStarted = false;
    private bool fading = false;
    private bool cutSceneOn = false;


    public void StartLevel()
    {
        StartCoroutine(LevelCoroutine());
    }

    IEnumerator LevelCoroutine()
    {
        playerController.StopPlayer();
        uiController.StartConversationWithColor(firstBadGuyConversation.dialouges);
        dialougeStarted = true;
        while (dialougeStarted)
            yield return null;
        int runPointIndex = 0;
        foreach (NPCController runner in npcRunners)
        {
            runner.OnRunTo(runPoints[runPointIndex]);
            runPointIndex = (runPointIndex + 1) % npcRunners.Length;
        }
        shopKeeper?.OnSit();
        uiController.FadeToBlack();
        fading = true;
        while (fading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        uiController.ToggleFadePanel(false);
        firstCutscene.PlayCutScene();
        cutSceneOn = true;
        while (cutSceneOn)
        {
            yield return new WaitForSeconds(0.1f);
        }
        uiController.ToggleFadePanel(true);
        foreach (var item in objectsToTurnOff)
        {
            item.SetActive(false);
        }


        yield return null;
    }

}
