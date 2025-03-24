using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



using System.Xml.Linq;
using UnityEngine.Windows.Speech;
public class SpeechRecognizer : MonoBehaviour
{
    public static SpeechRecognizer instance;
    private GrammarRecognizer grammarRecognizer;
    
    private void Start()
    {
        grammarRecognizer = new GrammarRecognizer(Application.streamingAssetsPath + "/SRGSText.xml");
        grammarRecognizer.OnPhraseRecognized += OnPhaseRecognized;
        Debug.Log("Successfully opened: /StreamingAssets/SRGSText.xml");
    }

    public void ResetGrammerRecognizer()
    {
        grammarRecognizer.OnPhraseRecognized -= OnPhaseRecognized;
        grammarRecognizer = null;

        grammarRecognizer = new GrammarRecognizer(Application.streamingAssetsPath + "/SRGSText.xml");
        grammarRecognizer.OnPhraseRecognized += OnPhaseRecognized;
        

    }
    
    private void OnPhaseRecognized(PhraseRecognizedEventArgs args)
    {
        //Debug.Log($"You said {args.text}");
        switch (args.text)
        {
            case "This is going to shoot a fireball":
                Debug.Log("Casting: Fireball Spell");
                break;
        
            case "I'm going to spawn a rock":
                Debug.Log("Casting: Rock Spell");
                break;
        
            case "I would like a glass of water":
                Debug.Log("Casting: Water Spell");
                break;
            
            case "This is not a registered spell":
                Debug.Log("Not a recognized phrase...");
                break;
            
            case "This is not going to work":
                Debug.Log("Not a recognized phrase...");
                break;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            grammarRecognizer.Start();

        if(Input.GetKeyUp(KeyCode.Mouse0))
            grammarRecognizer.Stop();
    }
    
    
    private string _input;
    
    public void ReadStringInput(string s)
    {
        _input = s;
        AddNewItemToXml(Application.streamingAssetsPath + "/SRGSText.xml", _input);

        ResetGrammerRecognizer();
    }
    
    void AddNewItemToXml(string filePath, string newItem)
    {
        XDocument doc = XDocument.Load(filePath);
        XNamespace ns = "http://www.w3.org/2001/06/grammar";

        XElement oneOf = doc.Descendants(ns + "one-of").FirstOrDefault();
        if (oneOf != null)
        {
            oneOf.Add(new XElement(ns + "item", newItem));
            doc.Save(filePath);
        }
    }
    
    void ClearXmlFileButKeepStructure(string filePath)
    {
        XDocument doc = XDocument.Load(filePath);
        XNamespace ns = "http://www.w3.org/2001/06/grammar";

        XElement oneOf = doc.Descendants(ns + "one-of").FirstOrDefault();
        if (oneOf != null)
        {
            oneOf.RemoveNodes(); // Removes all <item> elements but keeps <one-of>
            doc.Save(filePath);
        }
        
        //usage
        //ClearXmlFileButKeepStructure("path/to/your/file.xml");

    }
}
