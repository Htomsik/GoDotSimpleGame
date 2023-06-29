using Godot;

namespace SimpleGame.Scripts.Models.Weapon;

/// <summary>
///     Оружие
/// </summary>
public interface IWeapon 
{
    /// <summary>
    ///     Тип оружия
    /// </summary>
    public WeaponType WeaponType { get; }
    
    /// <summary>
    ///     Таймер, когда оружие атакует
    /// </summary>
    public Timer AttackTimer { get;  }

    /// <summary>
    ///     Атака
    /// </summary>
    /// <param name="direction">Направление атаки
    ///<remarks>Направление задается 1 или -1</remarks>
    /// </param>
    public void Attack(Vector2 direction);
    
    /// <summary>
    ///     Привязка к ноде
    /// </summary>
    /// <param name="owner">Нода родитель</param>
    public void SetOwner(Node2D owner);

    /// <summary>
    ///     Может ли оружие сейчас атаковать
    /// </summary>
    public bool CanAttack();
}

