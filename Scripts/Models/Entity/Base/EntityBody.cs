using System;
using Godot;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Entity
{
    public class EntityBody : KinematicBody2D
    {
        #region Properties

        public Action<float> PhysicsProcess { get; set; }
        
        public Action<float> Process { get; set; }
        
        public Action<InputEvent> Input { get; set; }

        #endregion

        #region Fields

        private readonly EntityData _data;
        
        private const int JumpPower = 16;
        
        
        private const int RunPower = 4;
        
        private const int StopRunFloorPower = 2;
        
        private const int StopRunAirPower = 1;

        private const int MaxRunPower = 16;
        
        
        private const int Gravity = 1;
        
        private readonly Vector2 _floor = new Vector2(0, -1);


        

        #endregion

        #region Constructors

        public EntityBody(EntityData data)
        {
            _data = data;
            
            AddChild(data.JubTimer);
            AddChild(data.HurtTimer);
        }

        #endregion

        #region Methods

        public override void _PhysicsProcess(float delta)
        {
            GravityApply();
            
            StopMoveApply();
            
            PhysicsProcess?.Invoke(delta);
            
            Move();
        } 

        public override void _Process(float delta) => Process?.Invoke(delta);

        public override void _Input(InputEvent ev) =>  Input?.Invoke(ev);

        
        private void Move()
        {
            UpdateLookDirection();
            
            SetAnimation();
            
            MoveAndSlide(_data.Velocity.Normalized() * EntityData.Speed, _floor);
        } 

        private void UpdateLookDirection()
        {
            if (_data.Velocity.x == 0) return;
            
            if (_data.HurtTimer.TimeLeft > 0)
            {
                _data.AnimatedSprite.FlipH = _data.Velocity.x > 0;
                return;
            } 
                
            _data.AnimatedSprite.FlipH = _data.Velocity.x < 0;

        }

        private void SetAnimation()
        {
            if (_data.HurtTimer.TimeLeft > 0)
            {
                _data.AnimatedSprite.Play(EntitySpriteNames.HurtSprite);
                return;
            }
            
            if (_data.JubTimer.TimeLeft > 0)
            {
                _data.AnimatedSprite.Play(EntitySpriteNames.JubSprite);
                return;
            }
            
            if (_data.Velocity.x  == 0 && _data.Velocity.y == 0 )
            {
                _data.AnimatedSprite.Play(EntitySpriteNames.IdleSprite);
                return;
            }
            
            if (IsOnFloor() && _data.Velocity.x != 0)
            {
                _data.AnimatedSprite.Play(EntitySpriteNames.RunSprite);
                return;
            }

            if (!IsOnFloor() && Math.Abs(_data.Velocity.y) > 1)
            {
                _data.AnimatedSprite.Play(_data.Velocity.y < 0 ? EntitySpriteNames.JumpStartSprite : EntitySpriteNames.JumpEndSprite);
                return;
            }
        }
        
        #region Сопротивления движению

        private void GravityApply()
        {
            if (IsOnFloor())
            {
                _data.Velocity.y = 0;
            }
            else
            {
                _data.Velocity.y += Gravity;
            }
        }

        private void StopMoveApply()
        {
            if (_data.Velocity.x == 0)
            {
                return;
            }
            
            if (IsOnFloor())
            {
                if (Math.Abs(_data.Velocity.x) <= 1)
                {
                    _data.Velocity.x += _data.Velocity.x > 0 ? - _data.Velocity.x  : _data.Velocity.x;
                    return;
                }
                
                _data.Velocity.x +=  _data.Velocity.x > 0 ? -StopRunFloorPower : StopRunFloorPower;
                return;
            }
            
            _data.Velocity.x +=  _data.Velocity.x > 0 ? -StopRunAirPower : StopRunAirPower;
        }


        #endregion

        #region Движения

        public void Jump(float jumpRate = 1.0f, bool ignoreFloor = false)
        {
            if (!ignoreFloor && !IsOnFloor()) return;
            
            if (_data.Velocity.x != 0)
            {
                _data.Velocity.y -= JumpPower * jumpRate * 1.5f;
            }
            else
            {
                _data.Velocity.y -= JumpPower * jumpRate;
            }
        }

        /// <summary>
        ///     Получение урона
        /// </summary>
        /// <param name="kickBackPower"></param>
        /// <param name="damagePosition"></param>
        public void Hurt(float kickBackPower, Vector2 damagePosition)
        {
            if (damagePosition.x > _data.Collider.GlobalPosition.x)
            {
                _data.Velocity.x -= kickBackPower;
            }
            else
            {
                _data.Velocity.x += kickBackPower;
            }
            
        }

        /// <summary>
        ///     Нанесение удара
        /// </summary>
        public bool Jub()
        {
            if (_data.JubTimer.TimeLeft > 0)
            {
                return false;
            }

            if (_data.Velocity.y != 0)
            {
                return false;
            }

            _data.Velocity.x = 0;
            
            _data.JubTimer.Start(0.3f);
            _data.JubTimer.OneShot = true;
            
            return true;
        }
        
        public void Run(float runRate = 1.0f)
        {
            if (Math.Abs(_data.Velocity.x) >= MaxRunPower || _data.JubTimer.TimeLeft > 0) return;
            
            if (IsOnFloor())
            {
                _data.Velocity.x += RunPower * runRate;
            }
            else
            {
                _data.Velocity.x += runRate;
            }
        }
        
        #endregion
        
        #endregion
    }
}