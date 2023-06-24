using Godot;

namespace SimpleGame.Scripts.Models.Entity
{
    public class EntityData
    {
        #region Properties

        public Sprite Sprite { get; private set; } = new Sprite();

        public CollisionShape2D Collider { get; private set; } = new CollisionShape2D();

        public CapsuleShape2D Shape { get; private set; } = new CapsuleShape2D();
        
        
        public Vector2 Velocity = new Vector2();

        public const float Speed = 120;
        
        #endregion

        #region Constructors

        public EntityData()
        {
            Collider.Shape = Shape;
            Collider.RotationDegrees = 90;
            //Collider.Disabled = true;//test
        }

        #endregion

        #region Methods

        public void InitCollider(float radius, float height)
        {
            Shape.Radius = radius;
            Shape.Height = height;
        }
        
        public void InitBody(int vFrames, int hFrames, Vector2 offsetPos)
        {
            Sprite.Vframes = vFrames;
            Sprite.Hframes = hFrames;
            Sprite.Position = offsetPos;
        }

        #endregion
    }
}