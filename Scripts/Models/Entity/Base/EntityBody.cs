using System;
using Godot;
using SimpleGame.Scripts.Models.Entity.Enemy;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Entity
{
    public abstract class EntityBody<TEntityData> : KinematicBody2D
        where TEntityData : EntityData
    {
        #region Actions

        public Action<float> PhysicsProcess { get; set; }
        
        public Action<float> Process { get; set; }
        
        public Action<InputEvent> Input { get; set; }
        
        public Action Ready { get; set; }

        #endregion
        
        #region Properties

        public  TEntityData Data
        { 
            get; 
            set; 
        }

        protected readonly int JumpPower  =  16;
        
        protected readonly int RunPower  =  4;
        
        protected readonly int StopRunFloorPower  = 2;
        
        protected readonly int StopRunAirPower  = 1;

        protected readonly int MaxRunPower   = 16;
        
        protected readonly int Gravity  = 1;
        
        protected readonly Vector2 Floor = new Vector2(0, -1);
        
        #endregion
        
        #region Methods
        
        #region Обработчики тиков

        public override void _PhysicsProcess(float delta)
        {
            GravityApply();
            
            StopMoveApply();
            
            PhysicsProcess?.Invoke(delta);
            
            Move();
        } 

        public override void _Process(float delta) => Process?.Invoke(delta);

        public override void _Input(InputEvent ev) =>  Input?.Invoke(ev);


        #endregion

        #region Другое

        public override void _Ready() => Ready?.Invoke();
        
        #endregion

        #region Обработчики действий 

        private void Move()
        {
            UpdateLookDirection();
            
            SetAnimation();
            
            MoveAndSlide(Data.Velocity.Normalized() * EntityData.Speed, Floor );
            
        } 

        private void UpdateLookDirection()
        {
            if (Data.Velocity.x == 0) return;
            
            if (Data.HurtTimer.TimeLeft > 0)
            {
                Data.AnimatedSprite.FlipH = Data.Velocity.x > 0;
                return;
            } 
                
            Data.AnimatedSprite.FlipH = Data.Velocity.x < 0;
        }

        protected virtual  void SetAnimation()
        {
            if (Data.HurtTimer.TimeLeft > 0)
            {
                Data.AnimatedSprite.Play(EntitySpriteNames.HurtSprite);
                return;
            }
            
            if (Data.Velocity is { x: 0, y: 0 } )
            {
                Data.AnimatedSprite.Play(EntitySpriteNames.IdleSprite);
                return;
            }
            
            if (IsOnFloor() && Data.Velocity.x != 0)
            {
                Data.AnimatedSprite.Play(EntitySpriteNames.RunSprite);
                return;
            }

            if (!IsOnFloor() && Math.Abs(Data.Velocity.y) > 1)
            {
                Data.AnimatedSprite.Play(Data.Velocity.y < 0 ? EntitySpriteNames.JumpStartSprite : EntitySpriteNames.JumpEndSprite);
                return;
            }
        }

        #endregion
        
        #region Действия

        public void Jump(float jumpRate = 1.0f, bool ignoreFloor = false)
        {
            if (!ignoreFloor && !IsOnFloor()) return;
            
            if (Data.Velocity.x != 0)
            {
                Data.Velocity.y -= JumpPower * jumpRate * 1.5f;
            }
            else
            {
                Data.Velocity.y -= JumpPower * jumpRate;
            }
        }
        
        public void Hurt(Vector2 damageVelocity, float damage)
        {
            damageVelocity *= damage;
            
            Data.Velocity += damageVelocity;
        }
        
        public virtual void Run(float runRate = 1.0f)
        {
            if (IsOnFloor())
            {
                Data.Velocity.x += RunPower * runRate;
            }
            else
            {
                Data.Velocity.x += runRate;
            }
        }
        
        #endregion
        
        #region Сопротивления движению

        private void GravityApply()
        {
            if (IsOnFloor())
            {
                Data.Velocity.y = 0;
            }
            else
            {
                Data.Velocity.y += Gravity;
            }
        }

        private void StopMoveApply()
        {
            if (Data.Velocity.x == 0 && Data.Velocity.y == 0)
            {
                return;
            }
            
            if (IsOnWall())
            {
                Data.Velocity.x = 0;
                return;
            }

            if (IsOnCeiling())
            {
                Data.Velocity.y = 0;
                return;
            }
            
            if (IsOnFloor())
            {
               
                
                if (Math.Abs(Data.Velocity.x) <= 1)
                {
                    Data.Velocity.x += Data.Velocity.x > 0 ? - Data.Velocity.x  : Data.Velocity.x;
                    return;
                }
                
                Data.Velocity.x +=  Data.Velocity.x > 0 ? -StopRunFloorPower : StopRunFloorPower;
                return;
            }

            if (Data.Velocity.x != 0)
            {
                Data.Velocity.x +=  Data.Velocity.x > 0 ? -StopRunAirPower : StopRunAirPower;
            }
        }
        
        #endregion
        
        #endregion
    }
}