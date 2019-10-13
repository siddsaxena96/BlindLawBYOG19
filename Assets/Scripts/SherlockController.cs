using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;
using UnityEngine.UI;
public class SherlockController : MonoBehaviour, IEventListener
{
    [SerializeField] private Transform[] spawn = null;
    [SerializeField] private Transform sherlock = null;
    [SerializeField] private Text sherlockText = null;
    [SerializeField] private Core.Events.EventWithParameteres OnSherlock = null;
    [SerializeField] private Core.Events.Event OnSherlockEnd = null;
    private string[] textToDisplay;
    private int textCount => textToDisplay.Length;

    private int currentTextCount = -1;


    public void OnEventRaised()
    {
        throw new System.NotImplementedException();
    }

    public void OnEventRaisedWithParameters(List<object> parameters)
    {
        if (parameters != null)
        {
            textToDisplay = (string[])parameters[0];
            StartCoroutine(SplitText());
            currentTextCount = -1;
        }
    }

    IEnumerator SplitText()
    {

        foreach (string text in textToDisplay)
        {
            float randTime = Random.Range(0.1f, 0.8f);
            yield return new WaitForSeconds(randTime);
            float timer = Random.Range(1.0f, 1.5f);
            StartCoroutine(SherlockText(text, timer));
        }
    }

    IEnumerator SherlockText(string text, float timer)
    {
        Transform temp = null;
        int index = GetNextCurrentTextCount();
        temp = Instantiate(sherlock, spawn[index].transform.position, Quaternion.identity);
        temp.SetParent(spawn[index]);
        sherlockText.text = text;
        ToggleText(true);
        yield return new WaitForSeconds(timer);
        ToggleText(false);
        Destroy(temp.gameObject);
    }

    int GetNextCurrentTextCount()
    {
        currentTextCount++;
        currentTextCount = currentTextCount % spawn.Length;
        return currentTextCount;
    }

    private void ToggleText(bool tog)
    {
        sherlock.gameObject.SetActive(tog);
    }

    // Start is called before the first frame update
    void Start()
    {
        OnSherlock?.RegisterListener(this);

        if (sherlock != null)
        {
            sherlockText = sherlock.GetComponent<Text>();
            if (sherlockText == null)
            {
                print("go child");
            }
        }

    }


}
