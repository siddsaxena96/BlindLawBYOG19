using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialougeData", menuName = "Data/DialougeData")]
public class DialougeAsset : ScriptableObject
{
    [SerializeField] public string[] dialouges = null;
}
