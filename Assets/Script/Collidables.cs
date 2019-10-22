using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidables : MonoBehaviour
{
    private readonly Collider2D[] _hits = new Collider2D[10];
    
    protected BoxCollider2D BoxCollider;
    protected SpriteRenderer SpriteRenderer;

    public ContactFilter2D filter2D;

    protected virtual void Start() 
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        BoxCollider.OverlapCollider(filter2D, _hits);

        for (var i = 0; i < _hits.Length; i++)
        {
            if (ReferenceEquals(_hits[i], null)) continue;

            Debug.Log(_hits[i].name);
            OnCollide(_hits[i]);

            // to clean the array 
            _hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D otherCollider)
    {
        
    }

}
