using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseSceneController : MonoBehaviour
{
    public void GoToCourt(){
        SceneManager.LoadScene("Scene7CourtRoom");        
    }

    public void GoToDeath(){
        SceneManager.LoadScene("Scene8Chamber");
    }
}
