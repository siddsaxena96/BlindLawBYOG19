using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneInstance : MonoBehaviour
{
    [SerializeField] private Animator cutSceneAnimator = null;
    [SerializeField] private CutSceneEventData cutSceneEvents = null;
    [SerializeField] private SpriteRenderer cutSceneSpriteRenderer = null;

    private int secondCount = 0;
    private int cutSceneCount = 0;
    private bool cutSceneOn = false;

    public void PlayCutScene()
    {
        StartCoroutine(PlayCutSceneCoroutine());
    }

    IEnumerator PlayCutSceneCoroutine()
    {
        cutSceneAnimator.gameObject.SetActive(true);
        secondCount = 0;
        while (cutSceneCount < cutSceneEvents.cutSceneEvents.Count)
        {
            if (secondCount == cutSceneEvents.cutSceneEvents[cutSceneCount].onSecond && !cutSceneOn)
            {
                cutSceneAnimator.enabled = false;
                cutSceneSpriteRenderer.sprite = cutSceneEvents.cutSceneEvents[cutSceneCount].showSprite;
                cutSceneOn = true;
                Debug.Log(secondCount);
                Debug.Log("Pause");
                while (cutSceneOn)
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
            secondCount++;
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);
        Debug.Log("Enumerator End");
    }

    private void Update()
    {
        if (Input.GetKeyDown(cutSceneEvents.cutSceneEvents[cutSceneCount].onKeyPress) && cutSceneOn)
        {
            Debug.Log("Lets Go");
            cutSceneOn = false;
            cutSceneAnimator.enabled = true;
            cutSceneSpriteRenderer.sprite = null;
        }
    }

    public void OnEndCutScene()
    {
        cutSceneOn = false;
        //StopCoroutine(PlayCutSceneCoroutine());
        cutSceneAnimator.gameObject.SetActive(false);
    }

}
