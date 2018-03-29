using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceDoor : MonoBehaviour {

    /*[SerializeField]
    private String m_Hypotheses;

    [SerializeField]
    private String m_Recognitions;

    private DictationRecognizer m_DictationRecognizer;*/

    private Dictionary<string, Action> keywordUnlocks = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    public Rigidbody doorRb;

    //public bool lock1;
    //public bool lock2;
    //public bool lock3;

    // Use this for initialization
    void Start ()
    {
        keywordUnlocks.Add("Unlock One", Unlock);

        keywordRecognizer = new KeywordRecognizer(keywordUnlocks.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();

        /*
        m_DictationRecognizer = new DictationRecognizer();

        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            m_Recognitions += text + "\n";
        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
            m_Hypotheses += text;
        };

        m_DictationRecognizer.DictationComplete += (completeCause) =>
        {
            if (completeCause != DictationCompletionCause.Complete)
            {
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completeCause);
            }
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: (0}; HResult = {1}.", error, hresult);
        };

        m_DictationRecognizer.Start();
		*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Unlock()
    {
        print("Listening!");

        //Rigidbody rb = GetComponent<Rigidbody>();
        doorRb.AddExplosionForce(1000, transform.position, 1);

    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordUnlocks[args.text].Invoke();
    }

}
