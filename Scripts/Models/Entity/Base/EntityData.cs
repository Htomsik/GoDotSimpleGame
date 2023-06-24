using Godot;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Entity
{
    public class EntityData
    {
        #region Properties

        public AnimatedSprite AnimatedSprite { get; private set; } = new AnimatedSprite();

        public CollisionShape2D Collider { get; private set; } = new CollisionShape2D();

        public CapsuleShape2D Shape { get; private set; } = new CapsuleShape2D();
        
        
        public Vector2 Velocity = new Vector2();

        public const float Speed = 120;
        
        #endregion

        #region Fields

        public const string RunSprite = "Run";
        
        public const string IdleSprite = "Idle";
        
        public const string JumpStartSprite = "JumpStart";
        
        public const string JumpEndSprite = "JumpEnd";

        #endregion

        #region Constructors

        public EntityData()
        {
            Collider.Shape = Shape;
            //Collider.RotationDegrees = 90;
            //Collider.Disabled = true;//test
        }

        #endregion

        #region Methods

        public void InitCollider(float radius, float height)
        {
            Shape.Radius = radius;
            Shape.Height = height;
        }
        
        public void InitBody(Vector2 offsetPos)
        {
            AnimatedSprite.Position = offsetPos;

            AnimatedSprite.Frames = new SpriteFrames();
            
            AnimatedSprite.Frames.LoadAnimationFrames(IdleSprite, "res://Sprites/Entity/Character Idle 48x48.png", true, true);
            
            AnimatedSprite.Frames.LoadAnimationFrames(RunSprite, "res://Sprites/Entity/run cycle 48x48.png", true, true);
            
            AnimatedSprite.Frames.LoadAnimationFrames(JumpStartSprite, "res://Sprites/Entity/player jump 48x48.png", false, true, 1, 1);
            
            AnimatedSprite.Frames.LoadAnimationFrames(JumpEndSprite, "res://Sprites/Entity/player jump 48x48.png", false, true, 3,3);
        }

        #endregion
    }
}