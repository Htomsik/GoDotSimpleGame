using System;
using Godot;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Entity.Enemy
{
    public class EnemyData : EntityData
    {
        #region Свйоства сущности
        
        public Action Dead { get; set; }
        
        public bool IsDead { get; set; } = false;

        public float DeadTime { get; protected set; } = 0.5f;
        
        public float Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                HpBar.Value = _hp;
                
                if (_hp <= 0 )
                {
                    Dead?.Invoke();
                }
            }
        }
        
        private float _hp = 100;
        
        public float PunchTime { get; protected set; } = 0.3f;
        
        #endregion


        #region Физические объекты

        public TextureProgress HpBar { get; protected set; } = new TextureProgress();

        public Vector2 HpBarScale { get; protected set; } = new (0.3f, 0.3f);
        
        public EnemyHitBox HitBox { get; protected set; } = new EnemyHitBox();
        
        public Timer DeadTimer { get; protected set; } = new Timer();
        
        public Timer PunchTimer { get; set; } = new Timer();
        
        #endregion
        
        #region Constructors

        public EnemyData()
        {
            Hp = 100;
        }

        #endregion
        
        public override void InitTextures(Vector2 offsetPos)
        {
            HpBar.TextureProgress_ = ImageLoader.LoadTexture("res://Sprites/HpBar/HpBar.png");

            HpBar.RectScale = HpBarScale;
            
            HpBar.RectPosition = new Vector2(- HpBar.TextureProgress_.GetWidth() /2f * HpBarScale.x, - Collider.ColliderShape.Height * 1.1f);
            
            base.InitTextures(offsetPos);
        }
    }
}