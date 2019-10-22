using UnityEngine;

public class Weapon : Collidables
{
    // Damage Struct
    public ItemWeapon currentWeapon;
    
    // Weapon Swing
    private Animator _animator;
    private static readonly int swing = Animator.StringToHash("Swing");
    private float _cooldown = .5f;
    private float _lastSwing;

    #region Singleton

    public static Weapon Instance;

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;
    }
    #endregion
    
    protected override void Start()
    {
        base.Start();
        _animator = GetComponentInParent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(0))
        {
            if (Time.time - _lastSwing > _cooldown)
            {
                _lastSwing = Time.time;
                Swing();
            }
        }
    }

    public void EquipItem(ItemWeapon weapon)
    {
        currentWeapon = weapon;
        SpriteRenderer.sprite = weapon.icon;
    }

    protected override void OnCollide(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Fighter") && otherCollider.name != "Player")
        {
            var dmg = new Damage
            {
                damageAmount = currentWeapon.damage,
                pushForce = currentWeapon.pushForce,
                origin = transform.position
            };
            
            otherCollider.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        _animator.SetTrigger(swing);
        
    }
}
