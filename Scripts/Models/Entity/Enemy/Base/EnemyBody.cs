using System;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Entity.Enemy;

public class EnemyBody : EntityBody<EnemyData>
{
    public bool CanAttack()
    {
        if (!Data.CurrentWeapon.CanAttack())
        {
            return false;
        }

        return Data.Velocity.y == 0;
    }
    
    public override void Run(float runRate = 1)
    {
        if (Math.Abs(Data.Velocity.x) >= MaxRunPower || Data.CurrentWeapon.AttackTimer.TimeLeft > 0) return;
        
        base.Run(runRate);
    }
    
    protected override void SetAnimation()
    {
        if (Data.DeadTimer.TimeLeft > 0)
        {
            Data.AnimatedSprite.Play(EntitySpriteNames.DeadSprite);
            return;
        }
        
        if (Data.CurrentWeapon.AttackTimer.TimeLeft > 0)
        {
            Data.AnimatedSprite.Play(Data.CurrentWeapon.GetSpriteName());
            return;
        }
        
        base.SetAnimation();
    }
    
}