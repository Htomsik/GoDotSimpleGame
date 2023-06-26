using Godot;

namespace SimpleGame.Scripts.Models.Entity.Enemy
{
    public interface IEnemyHitbox
    {
        void GetDamage(float damage, Vector2 damageRotation);
    }
}