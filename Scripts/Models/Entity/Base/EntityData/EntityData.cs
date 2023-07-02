using Godot;
using Godot.Collections;
using SimpleGame.Scripts.Models.Extensions;
using SimpleGame.Scripts.Models.Inventory;
using SimpleGame.Scripts.Models.World;

namespace SimpleGame.Scripts.Models.Entity
{
    public abstract class EntityData
    {
        #region Свойства сущности
        
        public const float Speed = 120;
        
        public float HurtTime { get; protected set; } = 0.3f;

        /// <summary>
        ///     На каких слоях находится объект
        /// </summary>
        public Array<WorldLayers> Layers { get; set; } = new() {WorldLayers.Entity};

        /// <summary>
        ///     С какими слоями взаимодействует
        /// </summary>
        public Array<WorldLayers> LayersMask { get; set; } = new() {WorldLayers.World};

        #endregion

        #region Физические объекты

        public AnimatedSprite AnimatedSprite { get; protected set; } = new AnimatedSprite();
        
        public EntityCollider Collider { get; set; } = new EntityCollider();
        
        public Vector2 Velocity = new Vector2();
        
        public Timer HurtTimer { get; set; } = new Timer();

        public IInventory Inventory { get; } = new Inventory.Inventory();
        
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