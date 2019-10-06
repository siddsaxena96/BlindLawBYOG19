using System.Collections.Generic;
using UnityEngine;

public class PreconditionManager : MonoBehaviour
{
    public static PreconditionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("PreconditionManager").AddComponent<PreconditionManager>();
            }
            return instance;
        }
    }
    private static PreconditionManager instance;
    public List<Precondition> preconditions = new List<Precondition>();

    private static bool isQuitting = false;

    private void OnDestroy()
    {
#if UNITY_EDITOR
        if (preconditions.Count > 0)
        {
            Debug.Log("Deinitializing " + preconditions.Count + " precondition(s)");
        }
#endif
        Deinitialize();
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
        Deinitialize();
    }

    //private void Initialize()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = new GameObject("PreconditionManager").AddComponent<PreconditionManager>();
    //        preconditions = new List<Precondition>();
    //    }
    //}

    private void Deinitialize()
    {
        instance = null;
        ClearHandlers();
        preconditions?.Clear();
    }

    public void AddPrecondition(Precondition precondition)
    {
        if (!isQuitting)
        {
            //Initialize();
            if (!preconditions.Contains(precondition))
            {
                preconditions.Add(precondition);
            }
        }
    }

    public void RemovePrecondition(Precondition precondition)
    {
        if (!isQuitting)
        {
            //Initialize();
            if (preconditions.Contains(precondition))
            {
                preconditions.Remove(precondition);
            }
        }
    }

    private void ClearHandlers()
    {
        if (preconditions != null)
        {
            if (preconditions.Count > 0)
            {
                for (int i = 0; i < preconditions.Count; i++)
                {
                    preconditions[i]?.Reset();
                }
            }
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Count Preconditions")]
    private void CountPreconditionsContext()
    {
        Debug.Log(string.Format("{0} preconditions added to PreconditionManager", preconditions.Count));
    }

    [ContextMenu("Count Preconditions", true)]
    private bool CountPreconditionsContextValidate()
    {
        return Application.isPlaying && preconditions.Count > 0;
    }
#endif
}
