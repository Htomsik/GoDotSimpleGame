using System;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Entity.Enemy;

public class EnemyBody : EntityBody<EnemyData>
{
    public bool CanJub()
    {
        if (Data.PunchTimer.TimeLeft > 0)
        {
            return false;
        }

        return Data.Velocity.y == 0;
    }
    
    public override void Run(float runRate = 1)
    {
        if (Math.Abs(Data.Velocity.x) >= MaxRunPower || Data.PunchTimer.TimeLeft > 0) return;
        
        base.Run(runRate);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (Data.DeadTimer.TimeLeft > 0 || Data.PunchTimer.TimeLeft > 0)
        {
            Data.Velocity.x = 0;
        }
        
        base._PhysicsProcess(delta);
    }

    protected override void SetAnimation()
    {
        if (Data.DeadTimer.TimeLeft > 0)
        {
            Data.AnimatedSprite.Play(EntitySpriteNames.DeadSprite);
            Data.Velocity.x = 0;
            return;
        }
        
        if (Data.PunchTimer.TimeLeft > 0)
        {
            Data.AnimatedSprite.Play(EntitySpriteNames.PunchSprite);
            Data.Velocity.x = 0;
            return;
        }

        base.SetAnimation();
    }
    
}