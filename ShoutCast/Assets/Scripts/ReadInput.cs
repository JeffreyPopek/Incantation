using System.Xml.Linq;
using System.IO;
using System.Linq;
using UnityEngine;

public class ReadInput : MonoBehaviour
{
    private string _input;
    
    public void ReadStringInput(string s)
    {
        _input = s;
        AddNewItemToXml(Application.streamingAssetsPath + "/SRGSText.xml", _input);

        SpeechRecognizer.instance.ResetGrammerRecognizer();
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
}
