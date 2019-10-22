using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // References
    public Player player;
    public Weapon weapon;
    public TextManager textManager;
    
    // Logic
    public int itemCount = 1;
    public int hitPoint;
    public int pesos;
    public int xp;

    #region Singleton
    
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public void ShowText(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        textManager.Show(msg, fontSize, color, pos, motion, duration);
    }
    
    /*
     * INT preferredSkin
     * INT pesos
     * INT xp
     * INT weaponLevel
     */
    public void SaveState()
    {
        var s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += xp.ToString() + "|";
        s += "0";
        
        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState")) return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change player Skin
        pesos = int.Parse(data[1]);
        xp = int.Parse(data[2]);
        // Change weapon level

        Debug.Log("LoadState");
    }
}
