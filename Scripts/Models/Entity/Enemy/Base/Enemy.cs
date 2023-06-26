

using Godot;
using SimpleGame.Scripts.Models.Hit.Bullet;
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
            AddChild(Data.PistolShootTimer);
            
            // Получение урона
            Data.HitBox.Damage += (damage, direction) =>
            {
                Data.HurtTimer.OneShot = true;
                Data.HurtTimer.Start(Data.HurtTime);
                
                Data.Hp -= damage;
                
                Body.Hurt(direction, damage);
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
            Vector2 direction;
            
            if (Data.AnimatedSprite.FlipH)
            {
                hitStartPosition = new Vector2(Data.Collider.Position.x - 15 + posX, Data.Collider.Position.y + posY);
                direction = new Vector2(-1, 0);
            }
            else
            {
                hitStartPosition = new Vector2(Data.Collider.Position.x + 15 + posX, Data.Collider.Position.y + posY);
                direction = new Vector2(1, 0);
            }
            
            punch.SetPosition(hitStartPosition);
            punch.ChangeStartDirection(direction);
            
            punch.ConnectToNode(Body);
        }

        protected void PistolShoot()
        {
            if (!Body.CanPistolShoot())
            {
                return;
            }
            
            Data.PistolShootTimer.Start(Data.PistolShootTime);
            Data.PistolShootTimer.OneShot = true;
            
            CreatePistolShoot();
        }

        protected virtual void CreatePistolShoot(int posX = 0, int posY = -6)
        {
            var pistolShoot = new Bullet();

            Vector2 hitStartPosition;
            Vector2 direction;
            
            if (Data.AnimatedSprite.FlipH)
            {
                hitStartPosition = new Vector2(Data.Collider.Position.x - 15 + posX, Data.Collider.Position.y + posY);
                direction = new Vector2(-1, 0);
            }
            else
            {
                hitStartPosition = new Vector2(Data.Collider.Position.x + 15 + posX, Data.Collider.Position.y + posY);
                direction = new Vector2(1, 0);
            }
            
            pistolShoot.SetPosition(hitStartPosition);
            pistolShoot.ChangeStartDirection(direction);
            
            pistolShoot.ConnectToNode(Body);
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