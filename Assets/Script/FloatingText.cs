using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    private Player player;
    
    public bool isActive;
    public GameObject GameObject;
    public Text fText;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        isActive = true;
        lastShown = Time.time;
        GameObject.SetActive(isActive);
    }

    private void Hide()
    {
        isActive = false;
        GameObject.SetActive(isActive); 
    }

    public void UpdateFloatingText()
    {
        if (!isActive) return;
        if (Time.time - lastShown > duration) Hide();

        GameObject.transform.position += motion * Time.deltaTime;
    }
}
