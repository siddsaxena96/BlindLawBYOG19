using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataRowPopulator : MonoBehaviour
{
    public Text[] rowElements;

    public void SetRow(string[] elements)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            rowElements[i].text = elements[i];
        }
    }    
}
