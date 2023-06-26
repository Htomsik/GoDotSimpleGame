using System;
using Godot;

namespace SimpleGame.Scripts.Models.Hit
{
    /// <summary>
    ///     Тело Удара
    /// <remarks>Отвечает за физические процессы, анимации и тд.</remarks>
    /// </summary>
    public class HitBody<THitData> : KinematicBody2D
    where THitData : HitData
    {
        #region Actions

        public Action Ready { get; set; }
        
        public Action PhysicsProcess { get; set; }

        #endregion

        #region Properties

        public  THitData Data { get; set; }

        #endregion

        #region Обработчики тиков
        
        public override void _PhysicsProcess(float delta)
        {
            PhysicsProcess?.Invoke();
            
            Move();
        }
        
        public override void _Ready() =>  Ready?.Invoke();
        
        #endregion
        
        #region Обработчики действий

        protected virtual void Move()
        {
            UpdateLookDirection();
        
            SetAnimation();
        
            MoveAndSlide(Data.Velocity);
        }
        
        
        protected virtual void UpdateLookDirection()
        {
            if (Data.Velocity.x == 0) return;
        
            Data.AnimatedSprite.FlipH = Data.Velocity.x < 0;
        }
        
        protected virtual void SetAnimation()
        {
        }

        #endregion
    }
}