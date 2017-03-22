using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using UnityEngine;

public class LanguageManager : Singleton<LanguageManager> {
    
    public Languages ChosenLanguage { get; set; }

    public TextAsset textsXmlFile;    
    private XmlDocument textsXml;
    private Hashtable texts;

    new void Awake()
    {
        base.Awake();
        textsXml = new XmlDocument();
        textsXml.LoadXml(textsXmlFile.text);
    }

    private LanguageManager() { }

    public void SetLanguage(Languages language)
    {
        ChosenLanguage = language;
        texts = LoadSpecificLanguageTextsFromXmlToHashtable(language, textsXml);
        textsXml = null;
    }

    private Hashtable LoadSpecificLanguageTextsFromXmlToHashtable(Languages language, XmlDocument xml)
    {
        Hashtable hashtable = new Hashtable();
        XmlNode specificLanguageNode = xml.DocumentElement[language.ToString().ToLower()];
        if (specificLanguageNode != null)
        {
            var textsEnumerator = specificLanguageNode.GetEnumerator();
            while (textsEnumerator.MoveNext())
            {
                XmlNode currentNode = (XmlNode) textsEnumerator.Current;
                if (currentNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement singleElement = (XmlElement) currentNode;
                    hashtable.Add(singleElement.GetAttribute("name"), singleElement.InnerText);
                }
            }
        }
        else
        {
            Debug.LogError("The specified language does not exist: " + language);
        }
        return hashtable;
    }

    public string GetWord(MultiLanguageTexts message)
    {
        return GetStringFromHashtable(texts, message.ToString().ToLower());
    }

    private string GetStringFromHashtable(Hashtable hashtable, string key)
    {
        if (!hashtable.ContainsKey(key))
        {
            Debug.LogError("The specified string does not exist: " + key);
            return String.Empty;
        }
        return (string) hashtable[key];
    }
}