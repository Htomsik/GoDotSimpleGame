using Godot;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Entity
{
    public abstract class EntityData
    {
        #region Свойства сущности
        
        public const float Speed = 120;
        
        public float HurtTime { get; protected set; } = 0.3f;
        
        #endregion

        #region Физические объекты

        public AnimatedSprite AnimatedSprite { get; protected set; } = new AnimatedSprite();
        
        public EntityCollider Collider { get; set; } = new EntityCollider();
        
        public Vector2 Velocity = new Vector2();
        
        public Timer HurtTimer { get; set; } = new Timer();

        #endregion

        
        #region Methods
        
        public virtual void InitTextures(Vector2 offsetPos)
        {
            const string entitySpritePath = "res://Sprites/Entity/";
            
            AnimatedSprite.Position = offsetPos;
            AnimatedSprite.Frames = new SpriteFrames();
            
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.IdleSprite, entitySpritePath + "Entity_Idle.png", true, true);
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.RunSprite, entitySpritePath + "Entity_Run.png", true, true);
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.JumpStartSprite, entitySpritePath + "Entity_Jump.png", false, true, 1, 1);
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.JumpEndSprite, entitySpritePath + "Entity_Jump.png", false, true, 3,3);
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.HurtSprite,entitySpritePath + "Entity_Hurt.png", true, true);
        }

        #endregion
    }
}