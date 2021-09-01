using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level8Chamber : MonoBehaviour, ILevelController
{
    [SerializeField] private UIController uiController = null;
    [SerializeField] private DialougeSequence finalSequence = null;
    [SerializeField] private DialougeSequence finalSequence2 = null;
    [SerializeField] private GameObject gunObject = null;
    [SerializeField] private CameraFollow cameraController = null;
    [SerializeField] private GameObject finaltext = null;
    [SerializeField] private AudioClip blindLawByVishesh = null;

    private bool dialougeOn = false;
    private bool fading = false;

    private void Start()
    {
        StartLevel();
        AudioController.Instance.PlayBgMusic(blindLawByVishesh);
    }

    public void StartLevel()
    {
        StartCoroutine(LevelCoroutine());
    }

    IEnumerator LevelCoroutine()
    {
        yield return new WaitForSeconds(2);
        uiController.FadeFromBlack();
        fading = true;
        while(fading)
            yield return null;
        uiController.StartConversationWithColor(finalSequence.dialouges);
        dialougeOn = true;
        while (dialougeOn)
            yield return null;
        gunObject.SetActive(true);
        uiController.StartConversationWithColor(finalSequence2.dialouges);
        dialougeOn = true;
        while (dialougeOn)
            yield return null;
        cameraController.Shake(1, 2);
        finaltext.SetActive(true);
        uiController.FadeToBlack();
        fading = true;
        while (fading)
            yield return null;
        Debug.Log("over");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("BadEnd");
        yield return null;
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
