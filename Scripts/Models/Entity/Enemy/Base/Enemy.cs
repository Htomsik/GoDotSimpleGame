using Godot;
using SimpleGame.Scripts.Models.Weapon;

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

            Data.SetCurrentWeaponOwner += () =>
            {
                Data.CurrentWeapon.SetOwner(Body);
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
        ///     Атака оружием
        /// </summary>
        protected virtual void Attack()
        {
            if (!Body.CanAttack())
            {
                return;
            }
            
            Data.CurrentWeapon.Attack(Data.AnimatedSprite.FlipH ? new Vector2(-1,1) : new Vector2(1,1));
        }
        
        public virtual void ChangeHotBarItem(int number)
        {
            Data.Inventory.HotBar.Select(number);

            if (Data.Inventory.HotBar.Current is IWeapon item)
            {
                Data.CurrentWeapon = item;
            }
            else
            {
                Data.CurrentWeapon = null;
            }
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