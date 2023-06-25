

using Godot;

namespace SimpleGame.Scripts.Models.Entity.Enemy
{
    public class Enemy : Entity
    {
        private new EnemyData Data { get; set; }
        
        protected override void InitializeData()
        {
            Data = new EnemyData();

            base.Data = this.Data;
            
            Body = new EntityBody(Data);
            
            // Иницилизация 
            Data.InitTextures( new Vector2(0,0));
            Data.Collider.ChangeSize(8f, 16);
            Data.HitBox.Collider.ChangeSize(9f, 17);
            
            // Получение урона
            Data.HitBox.Damage += (damage, damagePosition) =>
            {
                Data.HurtTimer.OneShot = true;
                Data.HurtTimer.Start(Data.HurtTime);
                
                Data.Hp -= damage;
                
                Body.Hurt(damage,damagePosition);
            };
            
            // Смерть сущности
            Data.Dead += () =>
            {
                Data.DeadTimer.OneShot = true;
                Data.DeadTimer.Start(Data.DeadTime);
                
                Data.IsDead = true;
            };

        }
        
        protected override void InitializeChild()
        {
            AddChild(Data.HpBar);
            AddChild(Data.HitBox);
            AddChild(Data.DeadTimer);
            base.InitializeChild();
        }
        
        public override void Process(float delta)
        {
            if (!Data.IsDead || Data.DeadTimer.TimeLeft > 0)
            {
                LifeProcess();
                base.Process(delta);
                return;
            }
            
            DeadProcess();
        }

        /// <summary>
        ///     Процессы, происходящие пок объект жив
        /// </summary>
        public virtual void LifeProcess()
        {
            
        }

        /// <summary>
        ///     Процессы, происходящие после смерти объекта
        /// </summary>
        public virtual void DeadProcess()
        {
            Body.QueueFree();
        }


        protected virtual void Jub()
        {
            if (!Body.Jub())
            {
                return;
            }

            CreateHit();
        }
        
        protected virtual void CreateHit(int posX = 0, int posY = 0)
        {
            var hit = new Hit.Hit();

            Vector2 hitStartPosition;
            
            if (Data.AnimatedSprite.FlipH)
            {
                hitStartPosition = new Vector2(Data.Collider.Position.x - 15 + posX, Data.Collider.Position.y + posY);
            }
            else
            {
                hitStartPosition = new Vector2(Data.Collider.Position.x + 15 + posX, Data.Collider.Position.y + posY);
            }
            
            hit.SetPosition(hitStartPosition);
            
            hit.ConnectToNode(Body);
        }
    }
}