using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Hit.Bullet;

public class BulletBody : HitBody<BulletData>
{
    public override void _PhysicsProcess(float delta)
    {
        Data.Velocity.x *= Data.SpeedRatio;
        
        base._PhysicsProcess(delta);
        
        Move();
    }

    protected virtual void Move()
    {
        SetAnimation();
        
        MoveAndSlide(Data.Velocity);
    }

    protected virtual void SetAnimation()
    {
        Data.AnimatedSprite.Play(EntitySpriteNames.PistolBulletSprite);
    }
}