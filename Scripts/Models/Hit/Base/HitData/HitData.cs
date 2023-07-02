using Godot;
using Godot.Collections;
using SimpleGame.Scripts.Models.World;

namespace SimpleGame.Scripts.Models.Hit
{
    /// <summary>
    ///     Данные удара
    /// <remarks>   Отвечает за хранение данные</remarks>
    /// </summary>
    public class HitData 
    {
        #region Свйоства сущности
        public float DamagePower { get; set; } = 10;
        
        public float LifeTime { get; set; } = 0.1f;
        
        
        /// <summary>
        ///     На каких слоях находится объект
        /// </summary>
        public Array<WorldLayers> Layers { get; set; } = new() {WorldLayers.Hit};

        /// <summary>
        ///     С какими слоями взаимодействует
        /// </summary>
        public Array<WorldLayers> LayersMask { get; set; } = new() {WorldLayers.Enemy, WorldLayers.Player};
        
        #endregion
        
        
        #region Физические объекты
        
        public AnimatedSprite AnimatedSprite { get; protected set; } = new AnimatedSprite();

        public Timer LifeTimer { get; set; } = new Timer();
        
        public HitHitBox HitBox { get; protected set; } = new HitHitBox();

        public HitCollider Collider { get; protected set; } = new HitCollider();
        
        public Vector2 Velocity = new Vector2();
        
        public Vector2 Direction = new Vector2();
        
        
        #endregion

        public HitData()
        {
            AnimatedSprite.Frames = new SpriteFrames();
        }


        #region Methods

        public virtual void InitTextures(Vector2 offsetPos)
        {
            AnimatedSprite.Position = offsetPos;
        }

        #endregion

    }
}