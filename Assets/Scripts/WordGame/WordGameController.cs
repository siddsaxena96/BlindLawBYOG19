using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordGameController : MonoBehaviour
{
    [SerializeField] private Text gameText = null;
    [SerializeField] private Button button1 = null;
    [SerializeField] private Button button2 = null;
    [SerializeField] private Button button3 = null;
    [SerializeField] private DataRowPopulator buttonRow = null;

    public void PlayWordGame(WordGameData wordGameData)
    {   
        StartCoroutine(WordGameCoroutine(wordGameData));
    }

    IEnumerator WordGameCoroutine(WordGameData wordGameData)
    {
        
        yield return null;
    }
}
