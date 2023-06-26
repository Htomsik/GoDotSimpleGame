

using Godot;
using SimpleGame.Scripts.Models.Hit.Punch;

namespace SimpleGame.Scripts.Models.Entity.Enemy
{
    public class Enemy : Entity<EnemyBody, EnemyData>
    {
        #region Constructors

        public Enemy() : base(new EnemyBody(), new EnemyData())
        {
            Data.InitTextures( new Vector2(0,0));
            Data.Collider.ChangeSize(8f, 16);
            Data.HitBox.Collider.ChangeSize(9f, 17);
            
            
            AddChild(Data.PunchTimer);
            
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

        #endregion
        
        #region Methods

        #region Обработчики тиков

        protected override void Process(float delta)
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
        ///     Процессы, происходящие пока объект жив
        /// </summary>
        protected virtual void LifeProcess()
        {
            
        }

        /// <summary>
        ///     Процессы, происходящие после смерти объекта
        /// </summary>
        protected virtual void DeadProcess()
        {
            Body.QueueFree();
        }

        #endregion

        #region Действия

        /// <summary>
        ///     Удар рукой
        /// </summary>
        protected virtual void Punch()
        {
            if (!Body.CanJub())
            {
                return;
            }
            
            Data.PunchTimer.Start(Data.PunchTime);
            Data.PunchTimer.OneShot = true;
            
            CreatePunch();
        }
        
        protected virtual void CreatePunch(int posX = 0, int posY = 0)
        {
            var punch = new Punch();

            Vector2 hitStartPosition;
            
            if (Data.AnimatedSprite.FlipH)
            {
                hitStartPosition = new Vector2(Data.Collider.Position.x - 15 + posX, Data.Collider.Position.y + posY);
            }
            else
            {
                hitStartPosition = new Vector2(Data.Collider.Position.x + 15 + posX, Data.Collider.Position.y + posY);
            }
            
            punch.SetPosition(hitStartPosition);
            
            punch.ConnectToNode(Body);
        }

        #endregion

        #region Другое

        protected override void Ready()
        {
            AddChild(Data.HpBar);
            AddChild(Data.HitBox);
            AddChild(Data.DeadTimer);
            
            base.Ready();
        }

        #endregion

        #endregion
    }
}