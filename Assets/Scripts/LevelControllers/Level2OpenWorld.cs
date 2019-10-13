using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Level2OpenWorld : MonoBehaviour, ILevelController
{
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private CutSceneInstance firstCutscene = null;
    [SerializeField] private NPCController[] npcRunners = null;
    [SerializeField] private Transform[] runPoints = null;
    [SerializeField] private NPCController shopKeeper = null;
    [SerializeField] private UIController uiController = null;
    [SerializeField] private DialougeSequence firstBadGuyConversation = null;
    [SerializeField] private DialougeSequence secondBadGuyConversation = null;
    [SerializeField] private DialougeSequence thirdBadGuyConversation = null;
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
        {
            yield return new WaitForSeconds(0.1f);
        }
        gunInHand.SetActive(true);
        int runPointIndex = 0;
        foreach (NPCController runner in npcRunners)
        {
            runner.OnRunTo(runPoints[runPointIndex]);
            runPointIndex = (runPointIndex + 1) % npcRunners.Length;
        }
        shopKeeper?.OnSit();
        yield return new WaitForSeconds(3);
        uiController.StartConversationWithColor(secondBadGuyConversation.dialouges);
        dialougeStarted = true;
        while (dialougeStarted)
        {
            yield return new WaitForSeconds(0.1f);

        }
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
        afterCutscene.SetActive(true);
        uiController.FadeFromBlack();
        fading = true;
        while (fading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        uiController.StartConversationWithColor(thirdBadGuyConversation.dialouges);
        dialougeStarted = true;
        while (dialougeStarted)
        {
            yield return new WaitForSeconds(0.1f);

        }
        uiController.FadeToBlack();
        fading = true;
        while (fading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene("SceneThreeChamber");
        Debug.Log("Enum khatam");
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

    public void OnEventRaisedWithParameters(List<object> parameters)
    {
        throw new System.NotImplementedException();
    }
}
