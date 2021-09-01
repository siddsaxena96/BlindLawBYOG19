using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class anyclick : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public string sceneName = null;
    public bool applicationCloseOnClick = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            if (!applicationCloseOnClick)
                SceneManager.LoadScene(sceneName);
            else
                Application.Quit(0);
        }
    }
}
