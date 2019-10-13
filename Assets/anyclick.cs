using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class anyclick : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public string sceneName = null;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            SceneManager.LoadScene(sceneName);
        }
    }    
}
