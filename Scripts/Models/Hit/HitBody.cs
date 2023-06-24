using Godot;

namespace SimpleGame.Scripts.Models.Hit
{
    public class HitBody : KinematicBody2D
    {
        public HitData Data { get; protected set; }

        public HitBody(HitData data)
        {
            Data = data;
        }
    }
}