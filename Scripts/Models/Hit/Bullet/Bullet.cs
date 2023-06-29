using Godot;

namespace SimpleGame.Scripts.Models.Hit.Bullet;

/// <summary>
///     Пуля
/// </summary>
public class Bullet : Hit<BulletData, BulletBody>
{
    public Bullet() : base(new BulletData(), new BulletBody())
    {
        Data.Collider.ChangeSize(1f, 7f);
        Data.HitBox.Collider.ChangeSize(1f,7f);
        Data.HitBox.Collider.RotationDegrees = 90f;
        
        Data.InitTextures( new Vector2(0,0));
    }
    
}