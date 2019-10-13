using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            yield return null;
        foreach (var item in objectsToTurnOff)
            item.SetActive(false);
        uiController.ToggleFadePanel(false);
        firstCutscene.PlayCutScene();
        cutSceneOn = true;
        while (cutSceneOn)
            yield return null;
        uiController.ToggleFadePanel(true);
        uiController.FadeFromBlack();
        fading = true;
        afterCutscene.SetActive(true);
        while (fading)
            yield return null;
        uiController.StartConversationWithColor(secondBadGuyConversation.dialouges);
        dialougeStarted = true;
        while(dialougeStarted)
            yield return null;
        yield return new WaitForSeconds(1);
        uiController.FadeToBlack();
        fading =true;
        while (fading)
            yield return null;
        SceneManager.LoadScene("CHOOSEFINALSCENE");
        Debug.Log("GG");

        yield return null;
    }

    public void OnDialougeOver()
    {
        dialougeStarted = false;
    }

    public void OnFadeOver()
    {
        fading = false;
    }

    public void OnCutSceneOver()
    {
        cutSceneOn = false;
    }
}
