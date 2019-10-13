using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Chamber : MonoBehaviour, ILevelController
{
    [SerializeField] private NPCController underStudyController = null;
    [SerializeField] private NPCController client1 = null;
    [SerializeField] private Transform tableStandingPoint = null;
    [SerializeField] private Transform wallStandingPoint = null;
    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private Transform chairLeft = null;
    [SerializeField] private Transform chairRight = null;
    [SerializeField] private UIController uiController;
    [SerializeField] private DialougeAsset firstDialouge = null;
    [SerializeField] private DialougeAsset secondDialouge = null;
    [SerializeField] private DialougeAsset thirdDialouge = null;

    private bool dialougeOn = false;
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
        uiController.FadeFromBlack();
        fading = true;
        while (fading)
            yield return null;
        uiController.StartConversation(firstDialouge.dialouges);
        dialougeOn = true;
        while (dialougeOn)
            yield return null;

        underStudyController.OnWalkTo(wallStandingPoint);
        client1.OnWalkTo(chairLeft);
        while (underStudyController.isWalking)
            yield return new WaitForSeconds(0.1f);
        Debug.Log("Flip Now");
        underStudyController.FlipSpriteX();

        while (client1.isWalking)
            yield return new WaitForSeconds(0.1f);

        client1.OnSit();
        yield return new WaitForSeconds(2);
        uiController.StartConversation(secondDialouge.dialouges);
        dialougeOn = true;
        while (dialougeOn)
            yield return null;
        client1.OnStand();
        yield return new WaitForSeconds(2);
        client1.FlipSpriteX();
        client1.OnWalkTo(spawnPoint);
        while (client1.isWalking)
            yield return null;
        uiController.StartConversation(thirdDialouge.dialouges);
        dialougeOn = true;
        while (dialougeOn)
            yield return null;
        yield return new WaitForSeconds(1);
        uiController.FadeToBlack();
        fading=true;
        while (fading)
            yield return null;
        SceneManager.LoadScene("Scene5CourtRoom");  
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
