using System;
using Godot;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Entity.Enemy
{
    public class EnemyData : EntityData
    {
        #region Properties

        public TextureProgress HpBar { get; private set; } = new TextureProgress();
        
        public EnemyHitBox HitBox { get; private set; } = new EnemyHitBox();
        
        public Action Dead { get; set; }


        #region HP

        public float Hp
        {
            get => _hp;

            set
            {
                if (_hp <= 0 )
                {
                    Dead?.Invoke();
                    return;
                }
                
                _hp = value;
                HpBar.Value = _hp;
            }
        }
        
        private float _hp = 100;

        #endregion
       

        #endregion
        
        
        #region Constructors

        public EnemyData()
        {
            Hp = 100;

            HitBox.Damage += (damage, _) =>
            {
                Hp -= damage;
            };
        }

        #endregion


        
        public override void InitBody(Vector2 offsetPos)
        {
            var rectScale = 0.3f;
            
            HpBar.TextureProgress_ = ImageLoader.LoadTexture("res://Sprites/HpBar/HpBar.png");
            
            HpBar.RectScale = new Vector2(rectScale, rectScale);
            
            HpBar.RectPosition = new Vector2(- HpBar.TextureProgress_.GetWidth() /2f * rectScale, - Collider.ColliderShape.Height * 1.1f);
            
            base.InitBody(offsetPos);
        }
    }
}