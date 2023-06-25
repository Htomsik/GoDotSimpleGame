using Godot;

namespace SimpleGame.Scripts.Models.Hit;

public class HitCollider : CollisionShape2D
{
    public CapsuleShape2D ColliderShape { get; protected set; } = new CapsuleShape2D();

    public HitCollider()
    {
        Shape = ColliderShape;
    }
        
    public HitCollider(CapsuleShape2D shape)
    {
        Shape = shape;
    }
        
    public void ChangeSize(float radius, float height)
    {
        ColliderShape.Radius = radius;
        ColliderShape.Height = height;
    }
}