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

        public Timer JubTimer { get; set; } = new Timer();
        
        public Timer HurtTimer { get; set; } = new Timer();
        
        #endregion

        
        #region Methods

        
        public virtual void InitBody(Vector2 offsetPos)
        {
            AnimatedSprite.Position = offsetPos;

            AnimatedSprite.Frames = new SpriteFrames();
            
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.IdleSprite, "res://Sprites/Entity/Character Idle 48x48.png", true, true);
            
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.RunSprite, "res://Sprites/Entity/run cycle 48x48.png", true, true);
            
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.JumpStartSprite, "res://Sprites/Entity/player jump 48x48.png", false, true, 1, 1);
            
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.JumpEndSprite, "res://Sprites/Entity/player jump 48x48.png", false, true, 3,3);
            
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.JubSprite,"res://Sprites/Entity/Player Jab 48x48.png", true, true);
            AnimatedSprite.Frames.SetAnimationSpeed(EntitySpriteNames.JubSprite,16f);
            
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.HurtSprite,"res://Sprites/Entity/Player Hurt 48x48.png", true, true);
        }

        #endregion
    }
}