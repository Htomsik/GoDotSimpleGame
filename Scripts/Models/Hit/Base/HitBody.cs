using System;
using Godot;

namespace SimpleGame.Scripts.Models.Hit
{
    /// <summary>
    ///     Тело Удара
    /// <remarks>Отвечает за физические процессы, анимации и тд.</remarks>
    /// </summary>
    public class HitBody : KinematicBody2D
    {
        #region Actions

        public Action Ready { get; set; }
        
        public Action PhysicsProcess { get; set; }

        #endregion
        
        
        private readonly HitData _data;
        
        public HitBody(HitData data)
        {
            _data = data;
        }

        public override void _PhysicsProcess(float delta) => PhysicsProcess?.Invoke();
        
        public override void _Ready() =>  Ready?.Invoke();
    }
}