using System;
using Godot;

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
        
        #endregion

        #region Constructors

        public EntityBody(EntityData data)
        {
            _data = data;
        }

        #endregion

        #region Methods

        public override void _PhysicsProcess(float delta)
        {
            PhysicsProcess?.Invoke(delta);
            
            if (_data.Velocity != Vector2.Zero)
                Move();
        } 

        public override void _Process(float delta) => Process?.Invoke(delta);

        public override void _Input(InputEvent ev) => Input?.Invoke(ev);

        private void Move()
        {
            UpdateLookDirection();
            
            MoveAndSlide(_data.Velocity.Normalized() * _data.Speed);
        } 

        private void UpdateLookDirection()
        {
            if (_data.Velocity.x != 0)
                _data.Sprite.FlipH = _data.Velocity.x < 0;
        }
        

        #endregion
    }
}