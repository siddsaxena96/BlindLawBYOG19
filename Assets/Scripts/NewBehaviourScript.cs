using System.Collections;
using System.Collections.Generic;
using Core.Events;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour, IEventListener
{
     public enum CourtEvents
    {
        Objection,
        Sherlock,
        CorrectOption
    }

    public Transform objectionPanel = null;
    public Transform objectionBar = null;
    private Animator objectionAnim = null;
    private readonly string objectiveAnimString = "objectiveAnimString";

    public Transform sherlockPanel = null;
    public Text sherlockText = null;
    public Animator sherlockAnim = null;

    public Transform correctOptionPanel = null;
    public Transform[] correctOptionButtons = null;
    
    

    [SerializeField] private Core.Events.Event OnObjectionRaised;

    public void OnEventRaised()
    {
        throw new System.NotImplementedException();
    }

    public void OnEventRaisedWithParameters(List<object> parameters)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnObjectionRaised?.RegisterListener(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
