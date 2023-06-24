using Godot;

namespace SimpleGame.Scripts.Models.Entity
{
    public class EntityCollider : CollisionShape2D
    {
        public CapsuleShape2D ColliderShape { get; protected set; } = new CapsuleShape2D();

        public EntityCollider()
        {
            Shape = ColliderShape;
        }
        
        public EntityCollider(CapsuleShape2D shape)
        {
            Shape = shape;
        }
        
        public void ChangeSize(float radius, float height)
        {
            ColliderShape.Radius = radius;
            ColliderShape.Height = height;
        }
    }
}