using System;
using Godot;
using SimpleGame.Scripts.Models.Extensions;
using SimpleGame.Scripts.Models.Weapon;

namespace SimpleGame.Scripts.Models.Entity.Enemy
{
    public class EnemyData : EntityData
    {
        #region Свйоства сущности
        
        public Action Dead { get; set; }
        
        public bool IsDead { get; set; } = false;
        
        public Action SetCurrentWeaponOwner { get; set; }
        
        public IWeapon CurrentWeapon
        {
            get => _currentWeapon;
            set
            {
                _currentWeapon = value;

                if (_currentWeapon is null)
                {
                    return;
                }
                
                SetCurrentWeaponOwner.Invoke();
            }
        }

        private IWeapon _currentWeapon;
        
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
        
        public float DeadTime { get; protected set; } = 2f;
        
        
        #endregion


        #region Физические объекты

        public TextureProgress HpBar { get; protected set; } = new TextureProgress();

        public Vector2 HpBarScale { get; protected set; } = new (0.3f, 0.3f);
        
        public EnemyHitBox HitBox { get; protected set; } = new EnemyHitBox();
        
        public Timer DeadTimer { get; protected set; } = new Timer();
        
        #endregion
        
        #region Constructors

        public EnemyData()
        {
            Hp = 10000;
        }

        #endregion

        
        public override void InitTextures(Vector2 offsetPos)
        {
            base.InitTextures(offsetPos);
            
            const string enemySpritePath = "res://Sprites/Enemy/";
            
            HpBar.TextureProgress_ = ImageLoader.LoadTexture(enemySpritePath + "Enemy_HpBar.png");
            HpBar.RectScale = HpBarScale;
            HpBar.RectPosition = new Vector2(- HpBar.TextureProgress_.GetWidth() /2f * HpBarScale.x, - Collider.ColliderShape.Height * 1.1f);
            
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.DeadSprite, enemySpritePath + "Enemy_Death.png", false, true);
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.PunchSprite,enemySpritePath + "Enemy_Punch.png", true, true);
            AnimatedSprite.Frames.SetAnimationSpeed(EntitySpriteNames.PunchSprite,16f);
            
            AnimatedSprite.Frames.LoadAnimationFrames(EntitySpriteNames.PistolShootSprite, enemySpritePath + "Enemy_PistolShoot.png", true, true);
            AnimatedSprite.Frames.SetAnimationSpeed(EntitySpriteNames.PistolShootSprite,16f);
        }
    }
}