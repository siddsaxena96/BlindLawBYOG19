using System.Collections;
using System.Collections.Generic;
using Core.Events;
using UnityEngine;
using UnityEngine.UI;

public class CourtEvent : MonoBehaviour, IEventListener
{
    public enum CourtEvents
    {
        Objection,
        Sherlock
    }

    public Transform objectionPanel = null;
    public Slider objectionBar = null;
    private Animator objectionAnim = null;
    private readonly string objectionAnimString = "objection";
    private bool waitForSpace = false;
    private bool spaceFound = false;
    private bool inRange = false;
    private bool returnSeek = false;
    [SerializeField] private KeyCode objectionKey = KeyCode.Space;
    private Image objectionImage = null;
    public Transform objectionKeyAnimation = null;
    public Core.Events.Event OnObjectionEnded;

    public Transform sherlockPanel = null;
    public Text sherlockText = null;
    public Animator sherlockAnim = null;
    [SerializeField] private Transform objectionIndicator = null;




    //[SerializeField] private Core.Events.Event OnObjectionRaised;
    [SerializeField] private Core.Events.EventWithParameteres OnSherlock, OnObjectionRaised;

    public void OnEventRaised()
    {
        throw new System.NotImplementedException();
    }

    public void OnEventRaisedWithParameters(List<object> parameters)
    {
        /*
        Sherlock format
        parameter[0] -option
        parameter[1] -strings
        parameter[2] -lag in btw alpha
        parameter[3] -lag in btw sentences

        objection
        [0] - op
        [1] - min
        [2] - max
        [3] - timer
         */


        if (parameters != null)
        {
            CourtEvents option = (CourtEvents)parameters[0];
            switch (option)
            {
                // case CourtEvents.Sherlock:
                //     string[] temp = (string[])parameters[1];
                //     float alphaLag = (float)parameters[2];
                //     float sentLag = (float)parameters[3];
                //     StartCoroutine(SherlockStrings(temp, alphaLag, sentLag));
                //     break;
                case CourtEvents.Objection:
                    int a = (int)parameters[1];
                    int b = (int)parameters[2];
                    float t = (float)parameters[3];
                    StartCoroutine(ObjectionTrigger(a, b, t));
                    break;
            }
        }
    }

    IEnumerator SherlockStrings(string[] strings, float alphaLag, float sentLag)
    {
        sherlockPanel.gameObject.SetActive(true);
        int len = strings.Length;
        foreach (string s in strings)
        {
            sherlockText.text = "";
            foreach (char c in s)
            {
                sherlockText.text += c;
                yield return new WaitForSeconds(alphaLag);
            }
            yield return new WaitForSeconds(sentLag);
        }
        sherlockPanel.gameObject.SetActive(false);


    }

    IEnumerator ObjectionTrigger(int min, int max, float timer)
    {
        //objectionAnim = objectionPanel.GetComponent<Animator>();
        objectionImage = objectionPanel.GetComponent<Image>();
        waitForSpace = false;
        spaceFound = false;
        inRange = false;
        returnSeek = false;
        objectionIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        objectionIndicator.gameObject.SetActive(false);
        objectionPanel.gameObject.SetActive(true);
        StartCoroutine(SlideTween(min, max, timer));
        yield return StartCoroutine(WaitForUserSpacebar(timer));
        if(spaceFound)
        {
            OnObjectionEnded?.Raise();
            //user clicked
        }
        else
        {
            // negative
        }
        objectionPanel.gameObject.SetActive(false);
    }

    IEnumerator WaitForUserSpacebar(float timer)
    {
        waitForSpace = true;
        yield return new WaitForSeconds(timer);
        waitForSpace = false;

    }

    IEnumerator SlideTween(int min, int max, float timer)
    {
        int i = 0;
        while (spaceFound == false)
        {
            if(i < 100)
                i++;
            if(i == 99)
                returnSeek = true;
            if(returnSeek)
                i--;
            if(i == 0)
                returnSeek = false;

            if (i <= max && i >= min)
                {
                    //objectionAnim.SetBool(objectionAnimString, true);
                    inRange = true;
                    objectionImage.color = Color.green;
                    objectionKeyAnimation.gameObject.SetActive(true);

                }
                else
                {
                    inRange = false;
                    //objectionAnim.SetBool(objectionAnimString, false);
                    objectionImage.color = Color.red;
                    objectionKeyAnimation.gameObject.SetActive(false);


                }
                objectionBar.SetValueWithoutNotify(i);
                yield return new WaitForSeconds(0.01f);

            
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        OnObjectionRaised?.RegisterListener(this);
        OnSherlock?.RegisterListener(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForSpace)
        {
            if (Input.GetKeyDown(objectionKey) && inRange)
            {
                spaceFound = true;         
            }
        }

    }
}
