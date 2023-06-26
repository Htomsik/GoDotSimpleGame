using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Hit.Bullet;

public class BulletBody : HitBody<BulletData>
{
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        
        Data.Velocity.x *= Data.SpeedRatio;

    }
    protected override void SetAnimation()
    {
        Data.AnimatedSprite.Play(EntitySpriteNames.PistolBulletSprite);
    }
}