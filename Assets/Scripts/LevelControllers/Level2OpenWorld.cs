using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2OpenWorld : MonoBehaviour, ILevelController
{
    [SerializeField] private PlayerController player = null;
    [SerializeField] private CutSceneInstance firstCutscene = null;

    public void StartLevel()
    {
        throw new System.NotImplementedException();
    }

    public void LoadFirstCutScene()
    {
        firstCutscene.PlayCutScene();
    }

}
