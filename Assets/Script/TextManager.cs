using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] private GameObject textContainer = null;
    [SerializeField] private GameObject textPrefab = null;
    [SerializeField] private Camera cam = null;
    
    private readonly List<FloatingText> _floatingTexts = new List<FloatingText>();
    
    

    private void Update()
    {
        foreach (var txt in _floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }

    private FloatingText GetFloatingText()
    {
        var txt = _floatingTexts.Find(t => !t.isActive);

        if (txt != null) return txt;
        
        txt = new FloatingText {GameObject = Instantiate(textPrefab)};
        txt.GameObject.transform.SetParent(textContainer.transform);
        txt.fText = txt.GameObject.GetComponent<Text>();
        
        _floatingTexts.Add(txt);

        return txt;
    }

    public void Show(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        var floatingText = GetFloatingText();

        floatingText.fText.text = msg;
        floatingText.fText.fontSize = fontSize;
        floatingText.fText.color = color;
      
        floatingText.GameObject.transform.position = cam.WorldToScreenPoint(pos);
        floatingText.motion = motion;
        floatingText.duration = duration;
        
        floatingText.Show();
    }
}
