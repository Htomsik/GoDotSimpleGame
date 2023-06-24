using Godot;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Entity
{
    public class EntityData
    {
        #region Properties

        public AnimatedSprite AnimatedSprite { get; protected set; } = new AnimatedSprite();
        
        public EntityCollider Collider { get; set; } = new EntityCollider();
        
        
        public Vector2 Velocity = new Vector2();

        public const float Speed = 120;
        
        #endregion

        
        #region Fields

        public const string RunSprite = "Run";
        
        public const string IdleSprite = "Idle";
        
        public const string JumpStartSprite = "JumpStart";
        
        public const string JumpEndSprite = "JumpEnd";

        #endregion

        
        #region Methods

        
        public virtual void InitBody(Vector2 offsetPos)
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