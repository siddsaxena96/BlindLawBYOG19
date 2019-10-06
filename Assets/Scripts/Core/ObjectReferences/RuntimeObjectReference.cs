using UnityEngine;

public class RuntimeObjectReference : MonoBehaviour
{
    /// <summary>
    /// The object reference asset.
    /// </summary>
    [Tooltip("The object reference asset.")]
    [SerializeField]
    private ObjectReference objectReference;

    /// <summary>
    /// Retrieves the ObjectReference.
    /// </summary>
    public ObjectReference ObjectReference
    {
        get => objectReference;
        set
        {
            objectReference = value;
            UpdateReference();
        }
    }

    private void OnValidate()
    {
        //if(objectReference == null)
        //{
        //    Debug.LogWarning("No ObjectReference set for " + name);
        //}
    }

    private void Awake()
    {
        UpdateReference();
    }

    private void OnEnable()
    {
        UpdateReference();
    }

    private void OnDestroy()
    {
        //ClearReference();
    }

    private void OnDisable()
    {

    }

    private void OnApplicationQuit()
    {
        ClearReference();
    }

    /// <summary>
    /// Updates the object reference to the GameObject it is attached to.
    /// </summary>
    public void UpdateReference()
    {
        if (objectReference != null)
        {
            objectReference.ReferenceObject = gameObject;
        }
    }

    /// <summary>
    /// Clears the reference object.
    /// </summary>
    public void ClearReference()
    {
        objectReference?.ClearReference();
    }

#if UNITY_EDITOR
    [ContextMenu("Print Target Object Name")]
    private void PrintTargetObjectNameContext()
    {
        if (objectReference != null)
        {
            if (objectReference.ReferenceObject)
            {
                Debug.Log(string.Format("[{0}] {1} | {2}", name, objectReference.ReferenceObject.name, objectReference.developerDescription));
            }
            else
            {
                Debug.Log(string.Format("[{0}] No ReferenceObject was set.", name));
            }
        }
        else
        {
            Debug.Log("ObjectReference is not set.");
        }
    }

    [ContextMenu("Print Target Object Name", true)]
    private bool PrintTargetObjectNameContextValidate()
    {
        return Application.isPlaying && objectReference != null;
    }
#endif
}
