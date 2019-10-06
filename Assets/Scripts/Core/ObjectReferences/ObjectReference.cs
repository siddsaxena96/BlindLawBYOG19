using UnityEngine;

[CreateAssetMenu(menuName = "Data/ObjectReference")]
public class ObjectReference : ScriptableObject
{
#if UNITY_EDITOR
    /// <summary>
    /// Developer description of the Event.
    /// </summary>
    [Tooltip(" Developer description of the Event.")]
    [Multiline]
    public string developerDescription = "";
#endif

    [Space]

    /// <summary>
    /// The reference object asset - to be assigned at runtime via scripting.
    /// </summary>
    [Tooltip("The reference object asset - to be assigned at runtime via scripting.")]
    [SerializeField]
    private GameObject referenceObject;

    /// <summary>
    /// Retrieves the reference object GameObject.
    /// </summary>
    public GameObject ReferenceObject
    {
        get => referenceObject;
        set => referenceObject = value;
    }

    /// <summary>
    /// Clears the reference object.
    /// </summary>
    public void ClearReference()
    {
        //if(ReferenceObject != null)
        //    ReferenceObject = null;

        //if (referenceObject != null)
        referenceObject = null;
    }

#if UNITY_EDITOR
    [ContextMenu("Print Object Reference Name")]
    private void PrintObjectReferenceNameContext()
    {
        if (referenceObject)
        {
            Debug.Log(referenceObject.name);
        }
    }

    [ContextMenu("Print Object Reference Name", true)]
    private bool PrintObjectReferenceNameContextValidate()
    {
        return Application.isPlaying;
    }
#endif
}
