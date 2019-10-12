using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;

public class CutSceneInstance : MonoBehaviour
{
    [SerializeField] private Animator cutSceneAnimator = null;
    [SerializeField] private CutSceneEventData cutSceneEvents = null;
    [SerializeField] private SpriteRenderer cutSceneSpriteRenderer = null;
    [SerializeField] private Core.Events.Event onCutSceneOver = null;

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
                while (cutSceneOn)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                cutSceneCount++;
            }
            secondCount++;
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);        
    }

    private void Update()
    {
        if (cutSceneOn && Input.GetKeyDown(cutSceneEvents.cutSceneEvents[cutSceneCount].onKeyPress))
        {            
            cutSceneOn = false;
            cutSceneAnimator.enabled = true;
            cutSceneSpriteRenderer.sprite = null;
        }
    }

    public void OnEndCutScene()
    {
        cutSceneOn = false;        
        onCutSceneOver?.Raise();
        Debug.Log("CutScene Over");
        cutSceneAnimator.gameObject.SetActive(false);
    }

}
