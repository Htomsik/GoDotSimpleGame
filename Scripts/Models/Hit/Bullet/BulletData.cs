using Godot;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Hit.Bullet;

public class BulletData : HitData
{
    #region Свойства сущности

    public float SpeedRatio { get; set; } = 1.2f;

    public float StartSpeed { get; set; } = 200f;
    
    #endregion
    
    public BulletData()
    {
        DamagePower = 30;
        LifeTime = 100;
        
        Velocity.x = StartSpeed;
    }
    
    public override void InitTextures(Vector2 offsetPos)
    {
        base.InitTextures(offsetPos);
            
        const string bulletSpritePath = "res://Sprites/Hit/Bullet/";
        
        AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.PistolBulletSprite, bulletSpritePath + "Bullet.png", false, true);
        AnimatedSprite.Scale = new Vector2(0.2f,0.2f);
    }
}