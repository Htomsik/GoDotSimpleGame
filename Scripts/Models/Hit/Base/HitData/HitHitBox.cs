using System;
using Godot;
using SimpleGame.Scripts.Models.Entity;
using SimpleGame.Scripts.Models.Entity.Enemy;


namespace SimpleGame.Scripts.Models.Hit
{
    public class HitHitBox : Area2D
    {
        public Action<Action<float, Vector2>> SetDamage { get; set; }
        
        public EntityCollider Collider { get; set; } = new EntityCollider();
        
        public HitHitBox()
        {
            AddChild(Collider);
            Connect("area_entered",this, nameof(AreaEntered));
        }
        
        public void AreaEntered(Area2D area2D)
        {
            if (area2D is IEnemyHitbox entityHitBox)
            {
                SetDamage?.Invoke(entityHitBox.GetDamage);
            }
        }
    }
}