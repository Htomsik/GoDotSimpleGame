

using Godot;

namespace SimpleGame.Scripts.Models.Entity.Enemy
{
    public class Enemy : Entity
    {
        private EnemyData EnemyData { get; set; }
        
        protected override void InitializeData()
        {
            EnemyData = new EnemyData();
            
            Data = EnemyData;
            Body = new EntityBody(Data);
            
            // Иницилизация 
            Data.InitBody( new Vector2(0,0));
            Data.Collider.ChangeSize(9f, 16);
            EnemyData.HitBox.Collider.ChangeSize(9f, 16);


            EnemyData.Dead += Dead;
            
            EnemyData.HitBox.Damage += (damage, damagePosition) =>
            {
                Data.HurtTimer.OneShot = true;
                Data.HurtTimer.Start(0.3f);
                
                Body.Hurt(damage,damagePosition);
            };

        }
        
        protected override void InitializeChild()
        {
            AddChild(EnemyData.HpBar);
            AddChild(EnemyData.HitBox);
            base.InitializeChild();
        }

        protected void Dead()
        {
            Body.QueueFree();
        }
        
        protected void Jub()
        {
            if (!Body.Jub())
            {
                return;
            }

            CreateHit();
        }
        
        protected void CreateHit()
        {
            var hit = new Hit.Hit();

            Vector2 jubPosition;

            if (Data.AnimatedSprite.FlipH)
            {
                jubPosition = new Vector2(Data.Collider.Position.x - 20, Data.Collider.Position.y);
            }
            else
            {
                jubPosition = new Vector2(Data.Collider.Position.x + 20, Data.Collider.Position.y);
            }
            
            hit.SetPosition(jubPosition);
            
            AddChild(hit.Body);
        }
    }
}