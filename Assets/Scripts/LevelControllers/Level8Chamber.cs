using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8Chamber : MonoBehaviour, ILevelController
{
    [SerializeField] private UIController uiController = null;
    [SerializeField] private DialougeSequence finalSequence = null;
    [SerializeField] private DialougeSequence finalSequence2 = null;
    [SerializeField] private GameObject gunObject = null;
    [SerializeField] private CameraFollow cameraController = null;
    [SerializeField] private GameObject finaltext = null;    
    
    private bool dialougeOn = false; 
    private bool fading = false;

    private void Start() {
        StartLevel();
    }

    public void StartLevel()
    {
        StartCoroutine(LevelCoroutine());
    }

    IEnumerator LevelCoroutine()
    {
        uiController.StartConversationWithColor(finalSequence.dialouges);
        dialougeOn = true;
        while(dialougeOn)
            yield return null;
        gunObject.SetActive(true);
        uiController.StartConversationWithColor(finalSequence2.dialouges);
        dialougeOn=true;
        while(dialougeOn)
            yield return null;
        cameraController.Shake(1,2);
        finaltext.SetActive(true);
        uiController.FadeToBlack();
        fading = true;
        while(fading)
            yield return null;
        Debug.Log("over");
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
