using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Dialouge
{
    [SerializeField] public string dialouge = null;
    [SerializeField] public Color dialougeColor = Color.black;

    Dialouge()
    {
        dialouge = null;
        dialougeColor = Color.white;
    }
}

[CreateAssetMenu(fileName = "DialougeData", menuName = "Data/DialougeSequence")]
public class DialougeSequence : ScriptableObject
{
    [SerializeField] public List<Dialouge> dialouges = new List<Dialouge>();
}
