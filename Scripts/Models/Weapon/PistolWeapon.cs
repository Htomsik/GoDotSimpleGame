﻿using Godot;
using SimpleGame.Scripts.Models.Hit.Bullet;

namespace SimpleGame.Scripts.Models.Weapon;

/// <summary>
///     Пистолет
/// </summary>
public class PistolWeapon : Weapon<Bullet>
{
    public PistolWeapon()
    {
        AttackDelay = 0.5f;
        WeaponType = WeaponType.Pistol;
        Offset = new Vector2(15,-6);
    }
}