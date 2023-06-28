using System;
using Godot;
using SimpleGame.Scripts.Models.Hit;

namespace SimpleGame.Scripts.Models.Weapon;

public abstract class Weapon<THit> : IWeapon
where THit : IHit, new()
{
    #region Свойства сущности

    protected float AttackDelay = 0f;
    
    public WeaponType Type { get; protected set; } = WeaponType.Punch;

    #endregion

    #region Физические свойства
    
    public Timer AttackTimer { get; } = new ();
    
    protected Node2D Owner { get;  set; }

    protected Vector2 Offset;

    #endregion
    
    protected Weapon()
    {
        AttackTimer.OneShot = true;
    }

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

    public bool CanAttack() => AttackTimer.TimeLeft <= 0;
}