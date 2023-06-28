using System;
using Godot;
using SimpleGame.Scripts.Models.CustomNode;

namespace SimpleGame.Scripts.Models.Weapon;

public interface IWeapon
{
    public WeaponType Type { get; }
    
    public Timer AttackTimer { get;  }

    public void Attack(Vector2 direction);

    
    public void SetOwner(Node2D owner);

    public bool CanAttack();
}

