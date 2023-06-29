using Godot;
using SimpleGame.Scripts.Models.Hit;
using SimpleGame.Scripts.Models.Item;

namespace SimpleGame.Scripts.Models.Weapon;

/// <summary>
///     Базовое оружие
/// </summary>
/// <typeparam name="THit">Патроны которыми атакует оружие</typeparam>
public abstract class Weapon<THit> : Item.Item, IWeapon
where THit : IHit, new()
{
    #region Свойства сущности

    /// <summary>
    ///     Сколько оружие атакует
    /// </summary>
    protected float AttackDelay = 0f;
    
    public WeaponType WeaponType { get; protected set; } = WeaponType.Punch;

    #endregion

    #region Физические свойства
    
    public Timer AttackTimer { get; } = new ();

    private Node2D Owner { get;  set; }

    protected Vector2 Offset;

    #endregion
    
    #region Constructors

    protected Weapon()
    {
        AttackTimer.OneShot = true;
        ItemType = ItemType.Weapon;
    }

    #endregion

    #region Methods

    public void Attack(Vector2 direction)
    {
        if (!CanAttack())
            return;
        
        var hit = new THit();
        
        hit.SetPosition(Offset * direction);
        hit.ChangeStartDirection(direction);
        hit.ConnectToNode(Owner);
        
        AttackTimer.Start(AttackDelay);
    }

    public void SetOffset(Vector2 offset) => Offset = offset;

    public void SetOwner(Node2D owner)
    {
        Owner = owner;
        Owner.AddChild(AttackTimer);
    }

    public void RemoveOwner()
    {
        Owner.RemoveChild(AttackTimer);
    }

    public bool CanAttack() => AttackTimer.TimeLeft <= 0;

    #endregion
}