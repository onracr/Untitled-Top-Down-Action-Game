using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portals : Collidables
{
    [SerializeField] private GameObject openedDoor = null, closedDoor = null;
    [SerializeField] private string[] sceneNames = null;
    
    protected override void OnCollide(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.name != "Player") return;
        
        GameManager.Instance.SaveState();
        closedDoor.SetActive(false);
        openedDoor.SetActive(true);
        var sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
        GameManager.Instance.player.transform.position = GameObject.Find("StartingPoint").transform.position;

        SceneManager.LoadScene(sceneName);
    }
}
