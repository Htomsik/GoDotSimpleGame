using System;
using Godot;

namespace SimpleGame.Scripts.Models.Entity.Enemy
{
    /// <summary>
    ///     Область получаемого урона
    /// </summary>
    public class EnemyHitBox : Area2D, IEnemyHitbox
    {
        public Action<float> Damage { get; set; }
        

        public EntityCollider Collider { get; set; } = new EntityCollider();
        
        public EnemyHitBox()
        {
            AddChild(Collider);
        }

        public void GetDamage(float damage)
        {
            Damage?.Invoke(damage);
        }
    }
}