using Godot;

namespace SimpleGame.Scripts.Models.Entity
{
    public class Enemy : Entity
    {
        public Enemy()
        {
            Data.InitBody( new Vector2(0,0));
            Data.InitCollider(6f, 18);    
        }
    }
}