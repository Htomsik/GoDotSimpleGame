using Godot;

namespace SimpleGame.Scripts.Models.Hit
{
    public class HitBody : KinematicBody2D
    {
        private readonly HitData _data;

        private readonly Timer _jubTimer = new Timer();

        public HitBody(HitData data)
        {
            _data = data;
            
            AddChild(_jubTimer);
        }

        public override void _PhysicsProcess(float delta)
        {
            if (_jubTimer.TimeLeft <= 0)
            {
                QueueFree();
                return;
            }
            
            base._PhysicsProcess(delta);
        }

        public override void _Ready()
        {
            _jubTimer.Start(_data.LifeTime);
            _jubTimer.OneShot = true;
            
            
            base._Ready();
        }
    }
}