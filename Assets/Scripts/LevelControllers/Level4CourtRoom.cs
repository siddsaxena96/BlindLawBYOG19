using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4CourtRoom : MonoBehaviour, ILevelController
{
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private UIController uIController = null;
    [SerializeField] private GameObject file = null;

    private bool highlightEvent = false;

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
        uIController.HighlighObject(file);
        highlightEvent = true;
        while (highlightEvent)
            yield return null;
        Debug.Log("Done");
        yield return null;
    }

    public void OnHighlightOver()
    {
        highlightEvent = false;
    }
}
