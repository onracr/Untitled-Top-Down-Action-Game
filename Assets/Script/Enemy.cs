using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement
{
    [SerializeField] private Player player = null;
    private Animator _animator;
    
    public int xpValue = 1;
    
    // Logic
    private bool _isChasing;
    private bool _isCollidingWithPLayer;
    private Transform _playerTransform;
    private Vector3 _startingPos;
    public float triggerDistance = .5f;
    public float chaseDistance = 2.0f;
    
    // HitBox
    private BoxCollider2D _hitBox;
    private Collider2D[] _hits = new Collider2D[10];
    public ContactFilter2D filter2D;
    
    // Cached
    private static readonly int EnemyRun = Animator.StringToHash("Enemy Run");
    private static readonly int EnemyIdle = Animator.StringToHash("Enemy Idle");

    protected override void Start()
    {
        base.Start();
        _playerTransform = player.transform;
        _startingPos = transform.position;
        _hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Is the player in range?
        if (Vector3.Distance(_playerTransform.position, _startingPos) < chaseDistance)
        {
            _hitBox.enabled = true;
            if (Vector3.Distance(_playerTransform.position, _startingPos) < triggerDistance)
                _isChasing = true;

            if (_isChasing)
            {
                
                if (!_isCollidingWithPLayer)
                    UpdateMotor((_playerTransform.position - transform.position).normalized);
                else
                    UpdateMotor(_startingPos);
            }
        }
        else
        {
            _hitBox.enabled = false;
            UpdateMotor(_startingPos - transform.position);
            _isChasing = false;
        }
        
        // Check for Movement
        CheckMovement();

        // Check for overlaps
        _isCollidingWithPLayer = false;
        _hitBox.OverlapCollider(filter2D, _hits);

        for (var i = 0; i < _hits.Length; i++)
        {
            if (_hits[i] == null) continue;

            if (_hits[i].CompareTag("Fighter") && _hits[i].name == player.name)
                _isCollidingWithPLayer = true;
            
            // to clean the array 
            _hits[i] = null;
        }
    }

    private void CheckMovement()
    {
        if (transform.hasChanged)
        {
            _animator.SetTrigger(EnemyRun);
            transform.hasChanged = false;
        }
        else
            _animator.SetTrigger(EnemyIdle);
        
    }
    

    protected override void Die()
    {
        Destroy(gameObject);
        GameManager.Instance.xp += xpValue;
        GameManager.Instance.ShowText(
            "+" + xpValue + " xp!", 30, Color.magenta, 
            player.transform.position, Vector3.up * 30, 1.0f);
    }
}
