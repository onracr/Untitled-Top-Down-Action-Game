using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : Fighter
{
    [SerializeField] private float ySpeed = .75f;
    [SerializeField] private float xSpeed = 1.0f;
    
    protected BoxCollider2D BoxCollider;
    private Vector3 _moveDelta;
    private RaycastHit2D _hit;
    
    protected override void Start()
    {
        base.Start();
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        _moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // Swap sprite direction, whether going right or left
        if (_moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (_moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetMouseButton(0) && _moveDelta.x > GetMousePosition().x)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (Input.GetMouseButton(0) && _moveDelta.x < GetMousePosition().x)
            transform.localScale = Vector3.one;

        // Add push vector, if any
        _moveDelta += PushDirection;
        
        // Reduce push force every frame, based off recovery speed
        PushDirection = Vector3.Lerp(PushDirection, Vector3.zero, pushRecoverySpeed);
        
        // Make sure we can move in this direction by casting a box there first, if the box returns null, we're free to move
        _hit = Physics2D.BoxCast(
            transform.position, BoxCollider.size, 0, new Vector2(0, _moveDelta.y), 
            Mathf.Abs(_moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking")
        );

        if (_hit.collider == null)
        {
            transform.Translate(0, _moveDelta.y * Time.deltaTime, 0);
        }

        _hit = Physics2D.BoxCast(
            transform.position, BoxCollider.size, 0, new Vector2(_moveDelta.x, 0),
            Mathf.Abs(_moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking")
        );

        if (_hit.collider == null) 
        {
            transform.Translate(_moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    private Vector3 GetMousePosition()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.x -= (float) Screen.width / 2;
        mousePosition.y -= (float) Screen.height / 2;
        
        return new Vector3(mousePosition.x / 80 , mousePosition.y / 80, 0);
    }
}
