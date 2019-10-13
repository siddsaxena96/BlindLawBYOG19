using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WordGameRow
{
    [SerializeField] public string[] words;
    [SerializeField] public int correctOption;
}

[CreateAssetMenu(fileName = "DialougeData", menuName = "Data/WordGameData")]
public class WordGameData : ScriptableObject
{
    [SerializeField] public WordGameRow[] wordGameRows = null;    
}
