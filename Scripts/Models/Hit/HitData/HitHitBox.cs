using Godot;
using SimpleGame.Scripts.Models.Entity.Enemy;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Hit
{
    public class HitHitBox : EnemyHitBox
    {

        public HitHitBox()
        {
            Connect("area_entered",this, nameof(AreaEntered));
        }
        
        public void AreaEntered(Area2D area2D)
        {
            if (area2D is IEnemyHitbox entityHitBox)
            {
                entityHitBox.GetDamage(10);
            }
        }
    }
}