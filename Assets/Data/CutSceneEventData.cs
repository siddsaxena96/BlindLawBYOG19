using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CutSceneEvent{
    [SerializeField] public float onSecond;
    [SerializeField] public Sprite showSprite;
    [SerializeField] public KeyCode onKeyPress;
}


[CreateAssetMenu(fileName = "DialougeData", menuName = "Data/CutSceneEventData")]
public class CutSceneEventData : ScriptableObject
{
    [SerializeField] public List<CutSceneEvent> cutSceneEvents = new List<CutSceneEvent>();
}
